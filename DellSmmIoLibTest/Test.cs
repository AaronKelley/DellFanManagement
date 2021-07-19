using DellFanManagement.SmmIo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DellSmmIoLibTest
{
    class Test
    {
        static void Main(string[] args)
        {
            try
            {
                //ThermalSetting();
                //RfInfo();
                //KeyboardBacklightInfo();

                Enumerate();
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine("{0}: {1}\n{2}", exception.GetType(), exception.Message, exception.StackTrace);
            }

            return;
        }

        private static void ThermalSetting()
        {
            Console.WriteLine(DellSmmIoLib.GetThermalSetting());
            Console.WriteLine();
        }

        private static void RfInfo()
        {
            DellSmmBiosMessage message = new DellSmmBiosMessage
            {
                Class = ClassToken.Info,
                Selector = SelectToken.RfKill
            };

            // RF information.
            DellSmmIoLib.ExecuteCommand(ref message);

            Console.WriteLine("{0}\t{1}\t{2}\t{3}", message.Output1, message.Output2, message.Output3, message.Output4);

            message = new DellSmmBiosMessage
            {
                Class = ClassToken.Info,
                Selector = SelectToken.RfKill,
                Input1 = 2
            };

            DellSmmIoLib.ExecuteCommand(ref message);

            Console.WriteLine("{0}\t{1}\t{2}\t{3}", message.Output1, message.Output2, message.Output3, message.Output4);

            Console.WriteLine();
        }

        /// <summary>
        /// https://elixir.bootlin.com/linux/latest/source/drivers/platform/x86/dell/dell-laptop.c#L1208
        /// </summary>
        private static void KeyboardBacklightInfo()
        {
            DellSmmBiosMessage message = new DellSmmBiosMessage
            {
                Class = ClassToken.KeyboardBacklight,
                Selector = SelectToken.KeyboardBacklight
            };

            DellSmmIoLib.ExecuteCommand(ref message);

            Console.WriteLine("Modes: {0}", message.Output2 & 0xFFFF);
            Console.WriteLine("Type: {0}", (message.Output2 >> 24) & 0xFF);
            Console.WriteLine("Triggers: {0}", message.Output3 & 0xFF);
            Console.WriteLine("Levels: {0}", (message.Output3 >> 16) & 0xFF);

            uint units = (message.Output3 >> 8) & 0xFF;

            Console.WriteLine("Seconds: {0}", (message.Output4 >> 0) & 0xFF);
            Console.WriteLine("Minutes: {0}", (message.Output4 >> 8) & 0xFF);
            Console.WriteLine("Hours: {0}", (message.Output4 >> 16) & 0xFF);
            Console.WriteLine("Days: {0}", (message.Output4 >> 24) & 0xFF);

            Console.WriteLine();
        }

        private static void Enumerate()
        {
            for (int classValue = 4; classValue < 256; classValue++)
            {
                for (int selectValue = 0; selectValue < 256; selectValue++)
                {
                    DellSmmBiosMessage message = new DellSmmBiosMessage
                    {
                        Class = (ClassToken)classValue,
                        Selector = (SelectToken)selectValue
                    };
                    Console.Write("{0}\t{1}\t", classValue, selectValue);

                    try
                    {
                        bool result = DellSmmIoLib.ExecuteCommand(ref message);
                        if (result)
                        {
                            Console.WriteLine("{0}\t{1}\t{2}\t{3}", message.Output1, message.Output2, message.Output3, message.Output4);
                        }
                        else
                        {
                            Console.WriteLine("Error");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Exception");
                    }
                }
            }
        }
    }
}
