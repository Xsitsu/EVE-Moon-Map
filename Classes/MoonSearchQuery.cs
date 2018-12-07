using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Classes
{
    public class MoonSearchQuery
    {
        public string SystemName { get; set; }
        public string OreName { get; set; }
        public int OreClassId { get; set; }
        public int Percentage { get; set; }
    }
}
