using LegoCrypto.Data.Model;
using System;
using System.Diagnostics;
using System.Threading;

namespace LegoCrypto.IO.Arduino
{
    public class ArduinoNFC : INfcDevice, IDisposable
    {
        public Stopwatch stopwatch { get; set; }
        private string _comPort;
        private int _baudRate;
        private int _timeOut;

        public ArduinoNFC()
        {

        }
        public ArduinoNFC(string comPort, int baudRate, int timeOut)
        {
            _comPort = comPort;
            _baudRate = baudRate;
            _timeOut = timeOut;
            stopwatch = new Stopwatch();
        }

        public bool CheckDevice()
        {
            using (var arduino = new ArduinoDriver(_comPort, _baudRate, _timeOut))
            {
                return (arduino.SendCommand(ArduinoCommands.LEGO_ARDUINO) == "MFRC522_V001");
            }
        }

        public ITag ReadNtag()
        {
            using (var arduino = new ArduinoDriver(_comPort, _baudRate, _timeOut))
            {
                var output = WaitForNtagHere(arduino);
                if (output == ArduinoCommands.NTAG_FOUND)
                {
                    return NtagRead(arduino);
                }
            }
            return null;
        }

        private ITag NtagRead(ArduinoDriver arduino)
        {
            var result = string.Empty;
            result = arduino.SendCommand(ArduinoCommands.NTAG_FULL);
            string[] SplitResult = result.Split('/', ' ');

            if (SplitResult.Length > 1)
            {

            }

            return TagFactory.CreateTag(result);
        }

        private string WaitForNtagHere(ArduinoDriver arduino)
        {
            var result = string.Empty;
            try
            {
                stopwatch.Restart();
                do
                {
                    result = arduino.SendCommand(ArduinoCommands.NTAG_HERE);

                    Thread.Sleep(100);

                    if (stopwatch.ElapsedMilliseconds > _timeOut)
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

        public void Dispose()
        {
            
        }
    }
}
