using EVE_Moon_Map.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Repository
{
    public interface IOreRepository
    {
        Dictionary<int, OreType> GetOreList();
        int GetOreId(string ore);

        Dictionary<int, string> GetOreClassList();
        int GetOreClassId(string oreClass);
    }
}
