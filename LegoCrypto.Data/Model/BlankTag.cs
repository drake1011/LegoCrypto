
namespace LegoCrypto.Data.Model
{
    internal class BlankTag : ITag
    {
        public uint? ID { get; private set; }
        public string UID { get; private set; }
        public DataRegisterCollection Pages { get; private set; }

        public BlankTag() => Pages = new DataRegisterCollection();

        public void Decrypt() => throw new System.NotImplementedException();

        public void Encrypt() => throw new System.NotImplementedException();
    }
}
