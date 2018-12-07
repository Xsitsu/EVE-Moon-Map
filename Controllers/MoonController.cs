using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE_Moon_Map.Classes;
using EVE_Moon_Map.Models;
using EVE_Moon_Map.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EVE_Moon_Map.Controllers
{
    public class MoonController : Controller
    {
        IOreRepository _repoOre;
        IWorldRepository _repoWorld;

        public MoonController(IOreRepository repoOre, IWorldRepository repoWorld)
        {
            _repoOre = repoOre;
            _repoWorld = repoWorld;
        }

        private List<string> GetOreList()
        {
            List<string> list = new List<string>();
            foreach (var item in _repoOre.GetOreList())
            {
                list.Add(item.Value.Name);
            }
            return list;
        }

        private List<string> GetOreClassList()
        {
            List<string> list = new List<string>();
            foreach (var item in _repoOre.GetOreClassList())
            {
                list.Add(item.Value);
            }
            return list;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search()
        {
            MoonSearchModel model = new MoonSearchModel
            {
                OreList = GetOreList(),
                OreTierList = GetOreClassList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Search(MoonSearchModel model)
        {
            if (!ModelState.IsValid)
            {
                return Search();
            }

            MoonSearchQuery query = new MoonSearchQuery()
            {
                SystemName = model.SystemName,
                OreName = model.OreName,
                OreClassId = _repoOre.GetOreClassId(model.OreTier),
                Percentage = model.Percentage
            };

            List<Sector> results = _repoWorld.Search(query);
            return View("SearchResults", results);
        }

        [HttpGet]
        public IActionResult Add()
        {
            MoonAddModel model = new MoonAddModel();

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(MoonAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add");
            }

            List<Sector> sectors = null;
            try
            {
                sectors = new MoonProbeDataDump(model).Parse();
            }
            catch (Exception)
            {
                //malformed data, just redirect
            }
             
            if (sectors == null)
            {
                return RedirectToAction("Add");
            }

            foreach (Sector sector in sectors)
            {
                if (!_repoWorld.SystemExists(sector.Name))
                {
                    _repoWorld.AddSystem(sector.Name);
                }
                int sectorId = _repoWorld.GetSystemId(sector.Name);

                foreach (var entryP in sector.Planets)
                {
                    int planetNumber = (int)entryP.Key;
                    Planet planet = entryP.Value;

                    if (!_repoWorld.PlanetExists(planetNumber, sectorId))
                    {
                        _repoWorld.AddPlanet(planetNumber, sectorId);
                    }
                    int planetId = _repoWorld.GetPlanetId(planetNumber, sectorId);

                    foreach (var entryM in planet.Moons)
                    {
                        int moonNumber = (int)entryM.Key;
                        Moon moon = entryM.Value;

                        if (!_repoWorld.MoonExists(moonNumber, planetId))
                        {
                            _repoWorld.AddMoon(moonNumber, planetId);
                        }
                        int moonId = _repoWorld.GetMoonId(moonNumber, planetId);

                        _repoWorld.DeleteMoonOre(moonId);

                        foreach (var ore in moon.GetOres())
                        {
                            int oreId = _repoOre.GetOreId(ore.Type.Name);
                            int percentage = (int)(ore.Percentage * 100);
                            _repoWorld.AddMoonOre(moonId, oreId, percentage);
                        }
                    }

                }
            }

            return RedirectToAction("Search");
        }
    }
}