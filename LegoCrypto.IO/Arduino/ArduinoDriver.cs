using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoCrypto.IO.Arduino
{
    public abstract class ArduinoDriver : IDisposable
    {
        private SerialPort Serial;
        private bool Reading_Ended { get; set; }
        private string Result { get; set; }
        public int TimeOut { get; set; }
        public string ReadToString { get; set; } = "\x03";

        public ArduinoDriver(string comPort, int baudRate, int timeOut)
        {
            try
            {
                TimeOut = timeOut;
                Serial = new SerialPort(comPort, baudRate, Parity.None, 8, StopBits.One);

                if (!Serial.IsOpen)
                {
                    Serial.DataReceived += new SerialDataReceivedEventHandler(Arduino_DataReceived);

                    try
                    {
                        Serial.Open();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Serial couldn't open", ex);
                    }
                }
                else throw new Exception("Error Serial is already open!");

            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        public string SendCommand(object obj)
        {
            Reading_Ended = false;

            if (Serial.IsOpen)
            {
                if (obj is ArduinoCommands)
                    Serial.Write(obj.ToString() + "\n");
                else if (obj is string)
                    Serial.Write((string)obj + "\n");
                else if (obj is byte[])
                    Serial.Write((byte[])obj, 0, ((byte[])obj).Length);
            }

            var sw = Stopwatch.StartNew();
            while (!Reading_Ended)
            {
                if (sw.ElapsedMilliseconds > TimeOut)
                {
                    sw.Stop();
                    Result = ArduinoCommands.TIMEOUT;
                    Reading_Ended = true;
                }
            }

            return Result;
        }

        void Arduino_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Result = Serial.ReadTo(ReadToString).Substring(1);
                Console.WriteLine("NFC_Serial_Response" + Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DataReceived" + ex.Message);
            }
            Reading_Ended = true;
        }

        public void Close()
        {
            Serial.Close();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Serial.IsOpen)
                        Close();
                }

                Serial = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
