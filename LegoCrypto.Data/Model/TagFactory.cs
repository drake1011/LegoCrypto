using System;

namespace LegoCrypto.Data.Model
{
    public static class TagFactory
    {
        public static ITag CreateTag(uint id, string uid)
        {
            if (id < 1000 && id > 0)
                return new CharacterTag(id, uid);
            else if (id >= 1000)
                return new TokenTag(id, uid);
            else
                throw new Exception("Error invalid ID");
        }

        public static ITag CreateTag(string data)
        {
            if (data.Length != 46)
                throw new ArgumentException(
                    string.Format("Not all data preset, Expecting UID and 4 register pages for a total of 46 characters, {0} characters returned {1}{2}", 
                    data.Length, Environment.NewLine, data));

            var uid = data.Substring(0, 14);
            var pages = new DataRegisterCollection();

            pages[DataRegister.Page35] = data.Substring(14, 8);
            pages[DataRegister.Page36] = data.Substring(22, 8);
            pages[DataRegister.Page37] = data.Substring(30, 8);
            pages[DataRegister.Page38] = data.Substring(38, 8);

            if (pages[DataRegister.Page38] == PageConstants.CharacterType)
                return new CharacterTag(uid, pages);
            else if (pages[DataRegister.Page38] == PageConstants.TokenType)
                return new TokenTag(uid, pages);
            else
                throw new Exception("Incorrect token type detected");
        }
    }
}
