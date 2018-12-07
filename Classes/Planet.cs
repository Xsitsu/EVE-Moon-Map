using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Classes
{
    public class Planet
    {
        public long Number { get; }
        public Dictionary<long, Moon> Moons = new Dictionary<long, Moon>();

        public Planet(long number)
        {
            Number = number;
        }
    }
}
