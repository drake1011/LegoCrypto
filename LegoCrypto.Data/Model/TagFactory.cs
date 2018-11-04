using LegoCrypto.Data.Crypto;
using System;
using System.Text;

namespace LegoCrypto.Data.Model
{
    public abstract class TagFactory
    {
        private static readonly uint TokenCutoff = 1000;

        public static ITag CreateTag(uint? id, string uid)
        {
            ValidateUID(uid);
            ValidateID(id);

            if (id < TokenCutoff)
                return new CharacterTag((uint)id, uid);
            else if (id >= TokenCutoff)
                return new TokenTag((uint)id, uid);
            else
                throw new ArgumentException("Error invalid ID");
        }

        public static ITagData CreateTag() => new BlankTag();

        public static ITag CreateTag(string data)
        {
            ValidateDataString(data);

            return 
                CreateTag(
                    data.Substring(0, 14),
                    data.Substring(14, 8),
                    data.Substring(22, 8),
                    data.Substring(30, 8),
                    data.Substring(38, 8)
                    );
        }

        public static ITag CreateTag(string uid, string dataPage35, string dataPage36, string dataPage37, string dataPage38)
        {
            ValidateUID(uid);
            ValidateDataPage(dataPage35);
            ValidateDataPage(dataPage36);
            ValidateDataPage(dataPage37);
            ValidateDataPage(dataPage38);

            var pages = new DataRegisterCollection();

            pages[DataRegister.Page35] = dataPage35;
            pages[DataRegister.Page36] = dataPage36;
            pages[DataRegister.Page37] = dataPage37;
            pages[DataRegister.Page38] = dataPage38;

            if (pages[DataRegister.Page38] == PageConstants.CharacterType)
                return new CharacterTag(uid, pages);
            else if (pages[DataRegister.Page38] == PageConstants.TokenType)
                return new TokenTag(uid, pages);
            else
                throw new Exception("Incorrect token type detected");
        }

        #region Validation

        private static bool ValidateDataString(string data)
        {
            if (data.Length != 46)
                throw new ArgumentException(
                    $"Not all data preset, Expecting UID and 4 register pages for a total of 46 characters, {data.Length} characters returned {Environment.NewLine}{data}");
            return true;
        }

        private static bool ValidateID(uint? id)
        {
            if ((id ?? 0) == 0)
                throw new ArgumentException("ID not set");
            return true;
        }

        private static bool ValidateUID(string uid)
        {
            if ((uid ?? string.Empty) == string.Empty)
                throw new ArgumentException("UID not set");
            if (uid?.Length != 14)
                throw new ArgumentException("UID not 14 characters");
            Bitwise.ConvertHexStringToByteArray(uid);
            return true;
        }

        public static bool ValidateDataPage(string data)
        {
            if(data?.Length != 8)
                throw new ArgumentException("Data Page not valid");
            Bitwise.ConvertHexStringToByteArray(data);
            return true;
        }

        #endregion
    }
}
