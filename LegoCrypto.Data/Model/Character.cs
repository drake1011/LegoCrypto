using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoCrypto.Data.Model
{
    public class Character : IToken
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public string World { get; set; }
    }
}
