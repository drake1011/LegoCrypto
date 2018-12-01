using System.IO;
using LegoCrypto.Data.Model;
using Newtonsoft.Json;

namespace LegoCrypto.Data
{
    internal static class TokenRepoData<T> where T : IToken
    {
        public static T[] LoadJson(string dataFile)
        {
            return JsonConvert.DeserializeObject<T[]>(File.ReadAllText(dataFile));
        }

        public static void WriteJson(T[] tokens, string dataFile)
        {
            var result = JsonConvert.SerializeObject(tokens);
            File.WriteAllText(dataFile, result);
        }
    }
}