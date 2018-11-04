using System;
using LegoCrypto.Data.Crypto;

namespace LegoCrypto.Data.Model
{
    internal class TokenTag : ITag
    {
        public uint? ID { get; private set; }
        public string UID { get; private set; }
        public DataRegisterCollection Pages { get; private set; }

        public TokenTag(uint id, string uid)
        {
            ID = id;
            UID = uid;
            Pages = new DataRegisterCollection();
            Pages[DataRegister.Page35] = PageConstants.DefaultEmpty;
            Pages[DataRegister.Page37] = PageConstants.DefaultEmpty;
            Pages[DataRegister.Page38] = PageConstants.TokenType;
        }

        public TokenTag(string uid, DataRegisterCollection pages)
        {
            UID = uid;
            Pages = pages;
        }

        public void Decrypt()
        {
            ID = CharCrypto.ReturnTokenUint(Pages[DataRegister.Page36]);
            Pages[DataRegister.Page43] = CharCrypto.PWDGen(UID);
        }

        public void Encrypt()
        {
            Pages[DataRegister.Page36] = CharCrypto.ReturnTokenHex(ID ?? 0);
            Pages[DataRegister.Page43] = CharCrypto.PWDGen(UID);
        }
    }
}
