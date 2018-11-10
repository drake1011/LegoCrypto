using System;
using LegoCrypto.Data.Crypto;

namespace LegoCrypto.Data.Model
{
    internal class VehicleTag : ITag
    {
        public uint? ID { get; private set; }
        public string UID { get; private set; }
        public DataRegisterCollection Pages { get; private set; }

        public VehicleTag(uint id, string uid)
        {
            ID = id;
            UID = uid;
            Pages = new DataRegisterCollection();
            Pages[DataRegister.Page35] = PageConstants.DefaultEmpty;
            Pages[DataRegister.Page37] = PageConstants.DefaultEmpty;
            Pages[DataRegister.Page38] = PageConstants.VehicleType;
        }

        public VehicleTag(string uid, DataRegisterCollection pages)
        {
            UID = uid;
            Pages = pages;
            ID = DecryptID();
        }

        private uint? DecryptID()
        {
            if (Pages[DataRegister.Page36]?.Length > 0)
                return CharCrypto.ReturnTokenUint(Pages[DataRegister.Page36]);
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
            Pages[DataRegister.Page36] = CharCrypto.ReturnTokenHex(ID ?? 0);
            Pages[DataRegister.Page43] = CharCrypto.PWDGen(UID);
        }
    }
}