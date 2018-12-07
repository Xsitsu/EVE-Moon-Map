using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Classes
{
    public class Sector
    {
        public string Name { get; }
        public Dictionary<long, Planet> Planets = new Dictionary<long, Planet>();

        public Sector(string name)
        {
            Name = name;
        }
    }
}
