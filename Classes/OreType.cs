using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Classes
{
    public class OreType
    {
        public string Name { get; }
        public string Class { get; }

        public OreType(string name, string c)
        {
            Name = name;
            Class = c;
        }
    }
}
