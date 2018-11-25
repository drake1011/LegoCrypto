using LegoCrypto.IO.Arduino;
using Xunit;

namespace LegoCrypto.UnitTests
{
    public class ArduinoTests
    {

        [Fact]
        public void Test()
        {
            using (var arduino = new ArduinoNFC("COM4", 9600, 1000))
            {
                var result = arduino.ReadNtag();
            }
        }
    }
}
