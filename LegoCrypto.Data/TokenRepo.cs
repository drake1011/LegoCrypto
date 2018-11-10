using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegoCrypto.Data.Model;
using Newtonsoft.Json;

namespace LegoCrypto.Data
{
    public static class TokenRepo<T> where T : IToken
    {
        public static T[] Load(string dataFile)
        {
            return JsonConvert.DeserializeObject<T[]>(File.ReadAllText(dataFile));
        }

        public static void Write(T[] tokens, string dataFile)
        {
            var result = JsonConvert.SerializeObject(tokens);
            File.WriteAllText(dataFile, result);
        }
    }
}
