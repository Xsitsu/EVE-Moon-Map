using EVE_Moon_Map.Classes;
using EVE_Moon_Map.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Repository
{
    public interface IWorldRepository
    {
        void AddSystem(string system);
        int GetSystemId(string system);
        bool SystemExists(string system);

        void AddPlanet(int planetNumber, int systemId);
        List<int> GetPlanetsInSystem(int systemId);
        int GetPlanetId(int planetNumber, int systemId);
        bool PlanetExists(int planetNumber, int systemId);

        void AddMoon(int moonNumber, int planetId);
        List<int> GetMoonsInPlanet(int planetId);
        int GetMoonId(int moonNumber, int planetId);
        bool MoonExists(int moonNumber, int planetId);

        void AddMoonOre(int moonId, int oreId, int percentage);
        void DeleteMoonOre(int moonId);
        Dictionary<int, int> GetOresInMoon(int moonId); // OreId, Percentage

        List<Sector> Search(MoonSearchQuery query);
    }
}
