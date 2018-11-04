
namespace LegoCrypto.Data.Model
{
    internal class BlankTag : ITagData
    {
        public uint? ID { get; private set; }
        public string UID { get; private set; }
        public DataRegisterCollection Pages { get; private set; }

        public BlankTag()
        {
            Pages = new DataRegisterCollection();
        }
    }
}
