using System;
using System.Collections.Generic;
using System.Text;

namespace LegoCrypto.IO.Arduino
{
    public sealed class ArduinoCommands
    {
        private readonly String name;
        private readonly int value;

        public static implicit operator string(ArduinoCommands op) { return op.name; }

        public static readonly ArduinoCommands LEGO_ARDUINO = new ArduinoCommands(1, "/LEGO_ARDUINO");
        public static readonly ArduinoCommands NTAG_HERE = new ArduinoCommands(2, "/NTAG_HERE");
        public static readonly ArduinoCommands NTAG_UID = new ArduinoCommands(3, "/NTAG_UID");
        public static readonly ArduinoCommands NTAG_HALT = new ArduinoCommands(3, "/NTAG_HALT");
        public static readonly ArduinoCommands NTAG_READ = new ArduinoCommands(3, "/NTAG_READ");
        public static readonly ArduinoCommands NTAG_READ_WITH_UID = new ArduinoCommands(3, "/UID_NTAG");
        public static readonly ArduinoCommands NTAG_WRITE = new ArduinoCommands(3, "/NTAG_WRITE");
        public static readonly ArduinoCommands NTAG_AUTH = new ArduinoCommands(3, "/NTAG_AUTH");
        public static readonly ArduinoCommands NTAG_FULL = new ArduinoCommands(3, "/NTAG_FULL");

        public static readonly ArduinoCommands ERROR = new ArduinoCommands(3, "ERROR");
        //public static readonly ArduinoCommands WAIT = new ArduinoCommands(3, "/WAIT");
        public static readonly ArduinoCommands END_WRITE = new ArduinoCommands(3, "END_WRITE");
        public static readonly ArduinoCommands TIMEOUT = new ArduinoCommands(3, "TIMEOUT");

        public static readonly ArduinoCommands NTAG_NOT_FOUND = new ArduinoCommands(3, "NO");
        public static readonly ArduinoCommands NTAG_FOUND = new ArduinoCommands(3, "YES");


        private ArduinoCommands(int value, String name)
        {
            this.name = name;
            this.value = value;
        }

        public override String ToString()
        {
            return name;
        }
    }
}
