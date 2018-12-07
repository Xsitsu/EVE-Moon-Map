using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Classes
{
    public class Moon
    {
        public long Number { get; }
        private List<Ore> _ores = new List<Ore>();

        public Moon(long number)
        {
            Number = number;
        }
        private Ore _FindOre(OreType type)
        {
            foreach (Ore ore in _ores)
            {
                if (ore.Type == type)
                {
                    return ore;
                }
            }
            return null;
        }
        public void AddOre(OreType type, double percentage)
        {
            _ores.Add(new Ore(type, percentage));
        }
        public bool HasOre(OreType type)
        {
            return (_FindOre(type) != null);
        }
        public Ore GetOre(OreType type)
        {
            return _FindOre(type);
        }
        public List<Ore> GetOres()
        {
            return _ores;
        }
    }
}
