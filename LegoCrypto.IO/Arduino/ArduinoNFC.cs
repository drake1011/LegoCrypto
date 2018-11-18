using LegoCrypto.Data.Model;
using System;
using System.Diagnostics;
using System.Threading;

namespace LegoCrypto.IO.Arduino
{
    public class ArduinoNFC : ArduinoDriver, INfcDevice
    {
        public Stopwatch stopwatch { get; set; }

        public ArduinoNFC(string comPort, int baudRate, int timeOut) : base(comPort, baudRate, timeOut)
        {
            stopwatch = new Stopwatch();
        }

        public ITag ReadNtag()
        {
            var output = WaitForNtagHere();
            if (output == ArduinoCommands.NTAG_FOUND)
            {
                return NtagRead();
            }
            return null;
        }

        private ITag NtagRead()
        {
            var result = string.Empty;
            result = SendCommand(ArduinoCommands.NTAG_FULL);
            string[] SplitResult = result.Split('/', ' ');

            if (SplitResult.Length > 1)
            {

            }

            return TagFactory.CreateTag(result);
        }

        private string WaitForNtagHere()
        {
            var result = string.Empty;
            try
            {
                stopwatch.Restart();
                do
                {
                    result = SendCommand(ArduinoCommands.NTAG_HERE);

                    Thread.Sleep(100);

                    if (stopwatch.ElapsedMilliseconds > TimeOut)
                        stopwatch.Stop();

                    if (!stopwatch.IsRunning)
                        result = ArduinoCommands.TIMEOUT;

                } while (result == ArduinoCommands.NTAG_NOT_FOUND);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}
