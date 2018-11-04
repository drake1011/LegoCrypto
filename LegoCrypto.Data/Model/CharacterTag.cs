using System;
using LegoCrypto.Data.Crypto;

namespace LegoCrypto.Data.Model
{
    internal class CharacterTag : ITag
    {
        public uint? ID { get; private set; }
        public string UID { get; private set; }
        public DataRegisterCollection Pages { get; private set; }

        public CharacterTag(uint id, string uid)
        {
            ID = id;
            UID = uid;
            Pages = new DataRegisterCollection();
            Pages[DataRegister.Page35] = PageConstants.DefaultEmpty;
            Pages[DataRegister.Page38] = PageConstants.CharacterType;
        }

        public CharacterTag(string uid, DataRegisterCollection pages)
        {
            UID = uid;
            Pages = pages;
            ID = DecryptID();
        }

        private uint? DecryptID()
        {
            if (Pages[DataRegister.Page36]?.Length > 0 && Pages[DataRegister.Page37]?.Length > 0)
                return CharCrypto.Decrypt(UID, Pages[DataRegister.Page36] + Pages[DataRegister.Page37]);
            else
                return ID;
        }

        public void Decrypt()
        {
            ID = DecryptID();
            Pages[DataRegister.Page43] = CharCrypto.PWDGen(UID);
        }

        public void Encrypt()
        {
            var result = CharCrypto.Encrypt(UID, ID ?? 0);
            Pages[DataRegister.Page36] = result[0];
            Pages[DataRegister.Page37] = result[1];
            Pages[DataRegister.Page43] = CharCrypto.PWDGen(UID);
        }
    }
}