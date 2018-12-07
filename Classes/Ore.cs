using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Classes
{
    public class Ore
    {
        public OreType Type { get; }
        public double Percentage { get; }
        public Ore(OreType type, double percentage)
        {
            Type = type;
            Percentage = percentage;
        }
    }
}
