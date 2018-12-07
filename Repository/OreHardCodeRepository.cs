using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE_Moon_Map.Classes;

namespace EVE_Moon_Map.Repository
{
    public class OreHardCodeRepository : IOreRepository
    {
        private string OreClass(int i)
        {
            switch(i)
            {
                case 1:
                    return "Non-Moon";
                case 2:
                    return "Ubiquitous";
                case 3:
                    return "Common";
                case 4:
                    return "Uncommon";
                case 5:
                    return "Rare";
                case 6:
                    return "Exceptional";
                default:
                    return "Unknown";
            }
        }

        public int GetOreId(string ore)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, OreType> GetOreList()
        {
            return new Dictionary<int, OreType>
            {
                [1] = new OreType("Stable Veldspar", OreClass(1)),
                [2] = new OreType("Glossy Scordite", OreClass(1)),
                [3] = new OreType("Opulent Pyroxeres", OreClass(1)),
                [4] = new OreType("Sparkling Plagioclase", OreClass(1)),
                [5] = new OreType("Platinoid Omber", OreClass(1)),
                [6] = new OreType("Resplendant Kernite", OreClass(1)),
                [7] = new OreType("Immaculate Jaspet", OreClass(1)),
                [8] = new OreType("Scintillating Hemorphite", OreClass(1)),
                [9] = new OreType("Lustrous Hedbergite", OreClass(1)),
                [10] = new OreType("Brilliant Gneiss", OreClass(1)),
                [11] = new OreType("Jet Ochre", OreClass(1)),
                [12] = new OreType("Dazzling Spodumain", OreClass(1)),
                [13] = new OreType("Pellucid Crokite", OreClass(1)),
                [14] = new OreType("Flawless Arkonor", OreClass(1)),
                [15] = new OreType("Cubic Bistot", OreClass(1)),
                [16] = new OreType("Mercoxit", OreClass(1)),

                [17] = new OreType("Bitumens", OreClass(2)),
                [18] = new OreType("Coesite", OreClass(2)),
                [19] = new OreType("Sylvite", OreClass(2)),
                [20] = new OreType("Zeolites", OreClass(2)),

                [21] = new OreType("Cobaltite", OreClass(3)),
                [22] = new OreType("Euxenite", OreClass(3)),
                [23] = new OreType("Scheelite", OreClass(3)),
                [24] = new OreType("Titanite", OreClass(3)),

                [25] = new OreType("Chromite", OreClass(4)),
                [26] = new OreType("Otavite", OreClass(4)),
                [27] = new OreType("Sperrylite", OreClass(4)),
                [28] = new OreType("Vanadinite", OreClass(4)),

                [29] = new OreType("Carnotite", OreClass(5)),
                [30] = new OreType("Cinnabar", OreClass(5)),
                [31] = new OreType("Pollucite", OreClass(5)),
                [32] = new OreType("Zircon", OreClass(5)),

                [33] = new OreType("Loparite", OreClass(6)),
                [34] = new OreType("Monazite", OreClass(6)),
                [35] = new OreType("Xenotime", OreClass(6)),
                [36] = new OreType("Ytterbite", OreClass(6))
            };
        }

        public Dictionary<int, string> GetOreClassList()
        {
            return new Dictionary<int, string>()
            {
                [1] = "Non-Moon",
                [2] = "Ubiquitous",
                [3] = "Common",
                [4] = "Uncommon",
                [5] = "Rare",
                [6] = "Exceptional",
            };
        }

        public int GetOreClassId(string oreClass)
        {
            throw new NotImplementedException();
        }
    }
}
