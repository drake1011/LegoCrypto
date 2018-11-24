using LegoCrypto.Data.Model;

namespace LegoCrypto.IO
{
    public interface INfcDevice
    {
        bool CheckDevice();
        ITag ReadNtag();
    }
}
