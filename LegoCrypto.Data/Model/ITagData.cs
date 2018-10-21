namespace LegoCrypto.Data.Model
{
    public interface ITagData
    {
        uint ID { get; }
        string UID { get; }
        DataRegisterCollection Pages { get; }
    }
}
