using LegoCrypto.Data.Model;

namespace LegoCrypto.IO
{
    interface INfcDevice
    {
        ITag ReadNtag();
    }
}
