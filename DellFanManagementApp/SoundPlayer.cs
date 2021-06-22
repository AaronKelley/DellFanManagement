using IrrKlang;
using System.Threading;

namespace DellFanManagement.App
{
    /// <summary>
    /// Used to play a sound.
    /// </summary>
    class SoundPlayer
    {
        /// <summary>
        /// Indicates that an external request has come in to terminate sound playback.
        /// </summary>
        private bool _terminationRequested;

        /// <summary>
        /// Optionally select a specific audio device to play the sound on.
        /// </summary>
        private readonly AudioDevice _audioDevice;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SoundPlayer(AudioDevice audioDevice = null)
        {
            _terminationRequested = false;
            _audioDevice = audioDevice;
        }

        /// <summary>
        /// Play a sound file.
        /// </summary>
        /// <param name="soundFilePath">Path to file to play</param>
        /// <param name="loop">Whether or not to loop the file (if true, method will never return)</param>
        public void PlaySound(string soundFilePath, bool loop = false)
        {
            ISoundEngine engine;

            if (_audioDevice != null)
            {
                engine = new(SoundOutputDriver.AutoDetect, SoundEngineOptionFlag.DefaultOptions, _audioDevice.DeviceId);
            }
            else
            {
                engine = new(SoundOutputDriver.AutoDetect, SoundEngineOptionFlag.DefaultOptions);
            }

            do
            {
                engine.Play2D(soundFilePath);
                while (!_terminationRequested && engine.IsCurrentlyPlaying(soundFilePath))
                {
                    Thread.Sleep(100);
                }

                if (_terminationRequested)
                {
                    engine.StopAllSounds();
                }
            } while (loop && !_terminationRequested);
        }

        /// <summary>
        /// Called externally to request termination of sound playback.
        /// </summary>
        public void RequestTermination()
        {
            _terminationRequested = true;
        }
    }
}
