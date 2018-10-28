using System;
using System.Text;

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
                    $"Not all data preset, Expecting UID and 4 register pages for a total of 46 characters, {data.Length} characters returned {Environment.NewLine}{data}");

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
            var sb = new StringBuilder();
            if (uid?.Length != 14)
                sb.AppendLine("UID not valid");
            if(dataPage35?.Length != 8)
                sb.AppendLine("DataPage35 not valid");
            if (dataPage36?.Length != 8)
                sb.AppendLine("DataPage36 not valid");
            if (dataPage37?.Length != 8)
                sb.AppendLine("DataPage37 not valid");
            if (dataPage38?.Length != 8)
                sb.AppendLine("DataPage38 not valid");

            if (sb.Length > 0)
                throw new ArgumentException(sb.ToString());

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
    }
}
