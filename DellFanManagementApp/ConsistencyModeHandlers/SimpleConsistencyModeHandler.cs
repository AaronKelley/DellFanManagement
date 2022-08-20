using DellFanManagement.App.FanControllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace DellFanManagement.App.ConsistencyModeHandlers
{
    /// <summary>
    /// Simple consistency mode handler, which just loads the CPU to drive the fan speed up.
    /// </summary>
    internal class SimpleConsistencyModeHandler : ConsistencyModeHandler
    {
        /// <summary>
        /// Total number of CPUs in the system.
        /// </summary>
        private readonly int _totalCpus;

        /// <summary>
        /// Number of threads to use to laod the CPU.
        /// </summary>
        private readonly int _threadCount;

        /// <summary>
        /// Whether or not the CPU-consuming threads should be running right now.
        /// </summary>
        private bool _loadCpu;

        /// <summary>
        /// A random number generator.
        /// </summary>
        private readonly Random _randomGenerator;

        /// <summary>
        /// List of running threads.
        /// </summary>
        private readonly List<Thread> _threads;

        /// <summary>
        /// Semaphore, used to make sure that thread indexing is set up properly.
        /// </summary>
        private readonly Semaphore _semaphore;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="core">Core object.</param>
        /// <param name="state">State object.</param>
        /// <param name="fanController">The fan controller in use.</param>
        public SimpleConsistencyModeHandler(Core core, State state, FanController fanController) : base(core, state, fanController)
        {
            // Determine how many CPU load threads we want...
            _totalCpus = Environment.ProcessorCount;
            Log.Write(string.Format("Processor count: {0}", _totalCpus));
            _threadCount = _totalCpus / 6;
            if (_threadCount == 0)
            {
                _threadCount = 1;
            }

            _loadCpu = false;
            _randomGenerator = new Random();
            _threads = new();
            _semaphore = new Semaphore(0, 1);
        }

        /// <summary>
        /// Run the consistency mode logic.  Fire up threads to heat up the CPU if the fan speed falls too low.
        /// </summary>
        public override void RunConsistencyModeLogic()
        {
            if (_core.RpmThreshold != null)
            {
                if (_state.Fan1Rpm == 0 || (_state.Fan2Present && _state.Fan2Rpm == 0))
                {
                    Log.Write(string.Format("Oops, hit zero [{0}] [{1}]", _state.Fan1Rpm, _state.Fan2Present ? _state.Fan2Rpm : "N/A"));
                }

                if (_state.Fan1Rpm > _core.RpmThreshold && (!_state.Fan2Present || _state.Fan2Rpm > _core.RpmThreshold))
                {
                    // All good.
                    _state.ConsistencyModeStatus = "Fans are operating above the requested threshold";
                    _loadCpu = false;
                    _core.TrayIconColor = TrayIconColor.Blue;
                }
                else
                {
                    // Fan speed is too low.
                    if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline)
                    {
                        // System is on battery power, don't load the CPU.
                        _state.ConsistencyModeStatus = "Fan speed is below the threshold, but the system is running on battery power";
                        _loadCpu = false;
                    }
                    else
                    {
                        // Need to raise the fan speed.
                        _state.ConsistencyModeStatus = string.Format("Loading the CPU to raise the fan speed ({0} thread{1})...", _threadCount, _threadCount > 1 ? "s" : string.Empty);
                        _loadCpu = true;
                    }

                    _core.TrayIconColor = TrayIconColor.Red;
                }
            }

            if (_loadCpu)
            {
                if (_threads.Count == 0)
                {
                    // Threads are not running.  Start some threads.
                    Log.Write(string.Format("Starting background threads [{0}] [{1}]", _state.Fan1Rpm, _state.Fan2Present ? _state.Fan2Rpm : "N/A"));
                    Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;
                    for (int index = 0; index < _threadCount; index++)
                    {
                        int startCpu = _totalCpus * index / _threadCount;
                        int endCpu = _totalCpus * (index + 1) / _threadCount;
                        int cpu = _randomGenerator.Next(startCpu, endCpu);
                        Thread thread = new(() => BackgroundWork(index, cpu));
                        thread.Priority = ThreadPriority.Lowest;
                        _threads.Add(thread);
                        thread.Start();
                        _semaphore.WaitOne();
                    }
                }
            }
            else
            {
                if (_threads.Count > 0)
                {
                    // Threads should stop automatically.
                    foreach (Thread thread in _threads)
                    {
                        thread.Join();
                    }

                    _threads.Clear();

                    Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
                    Log.Write(string.Format("Stopped background threads [{0}] [{1}]", _state.Fan1Rpm, _state.Fan2Present ? _state.Fan2Rpm : "N/A"));
                }
            }
        }

        /// <summary>
        /// Load the CPU.
        /// </summary>
        /// <param name="threadId">Index of the running thread.</param>
        /// <param name="cpu">Index of the CPU core that should be loaded.</param>
        private void BackgroundWork(int threadId, int cpu)
        {
            // Assign to a specific CPU core.
            Thread.BeginThreadAffinity();
            foreach (ProcessThread processThread in Process.GetCurrentProcess().Threads)
            {
                if (processThread.Id == GetCurrentThreadId())
                {
                    int affinity = 0x1 << cpu;
                    processThread.IdealProcessor = cpu;
                    processThread.ProcessorAffinity = (IntPtr)affinity;
                    Log.Write(string.Format("Thread {0}, affinity {1}", threadId, affinity.ToString("x8")));
                }
            }
            _semaphore.Release();

            long number = 0;
            Random random = new();

            // https://stackoverflow.com/a/49204137/4647297
            while (_loadCpu)
            {
                number += random.Next(100, 1000);
                if (number > 1000000) { number = 0; }
            }

            Thread.EndThreadAffinity();
        }

        /// <summary>
        /// Retrieves the thread identifier of the calling thread.
        /// </summary>
        /// <returns>The return value is the thread identifier of the calling thread.</returns>
        /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-getcurrentthreadid"/>
        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();
    }
}
