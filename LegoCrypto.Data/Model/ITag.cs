namespace LegoCrypto.Data.Model
{
    public interface ITag : ITagData
    {
        void Decrypt();
        void Encrypt();
    }
}
