using DellFanManagement.SmmIo;
using DellFanManagement.SmmIo.DellSmi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

                //Enumerate();
                //ReadTokens();
                //SetTokens();

                Console.WriteLine(DellSmmIoLib.GetPasswordFormat(SmiPassword.Admin));
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine("{0}: {1}\n{2}", exception.GetType(), exception.Message, exception.StackTrace);
            }

            return;
        }

        private static void ReadTokens()
        {
            Token[] tokens = new Token[] { Token.DiskControllerModeAhci, Token.DiskControllerModeRaid, Token.KeyboardIlluminationOn,
                Token.KeyboardIlluminationOff, Token.KeyboardIlluminationAuto, Token.KeyboardIlluminationAuto25, Token.KeyboardIlluminationAuto50,
                Token.KeyboardIlluminationAuto75, Token.KeyboardIlluminationAuto100, Token.FanControlOverrideEnable, Token.FanControlOverrideDisable,
                Token.FanSpeedLow, Token.FanSpeedMediumLow, Token.FanSpeedMedium, Token.FanSpeedMediumHigh, Token.FanSpeedHigh};

            foreach (Token token in tokens)
            {
                Console.WriteLine(token);
                DellSmmIoLib.GetToken(token);
                Console.WriteLine();
            }
        }

        private static void SetTokens()
        {
            for (int index = 0; index < 10; index++)
            {
                Token token = index % 2 == 0 ? Token.KeyboardIlluminationAuto50 : Token.KeyboardIlluminationAuto100;
                uint value = (uint)(index % 2 == 0 ? 6 : 8);

                DellSmmIoLib.GetToken(token);
                DellSmmIoLib.SetToken(token, 1);
                //DellSmmIoLib.SetToken(token, value, SelectToken.AC);
                DellSmmIoLib.GetToken(token);
                Thread.Sleep(1000);
            }
        }

        private static void ThermalSetting()
        {
            Console.WriteLine(DellSmmIoLib.GetThermalSetting());
            Console.WriteLine();
        }

        private static void RfInfo()
        {
            SmiObject message = new SmiObject
            {
                Class = Class.Info,
                Selector = Selector.RfKill
            };

            // RF information.
            DellSmmIoLib.ExecuteCommand(ref message);

            Console.WriteLine("{0}\t{1}\t{2}\t{3}", message.Output1, message.Output2, message.Output3, message.Output4);

            message = new SmiObject
            {
                Class = Class.Info,
                Selector = Selector.RfKill,
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
            SmiObject message = new SmiObject
            {
                Class = Class.KeyboardBacklight,
                Selector = Selector.KeyboardBacklight
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
                    SmiObject message = new SmiObject
                    {
                        Class = (Class)classValue,
                        Selector = (Selector)selectValue
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
