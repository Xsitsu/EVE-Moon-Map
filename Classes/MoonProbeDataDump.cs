using EVE_Moon_Map.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Classes
{
    public class MoonProbeDataDump
    {
        string _data { get; set; }

        public MoonProbeDataDump(MoonAddModel model)
        {
            _data = model.Data;
        }





        private static Dictionary<char, int> RomanMap = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        public static int RomanToInteger(string roman)
        {
            int number = 0;
            for (int i = 0; i < roman.Length; i++)
            {
                if (i + 1 < roman.Length && RomanMap[roman[i]] < RomanMap[roman[i + 1]])
                {
                    number -= RomanMap[roman[i]];
                }
                else
                {
                    number += RomanMap[roman[i]];
                }
            }
            return number;
        }

        public List<Sector> Parse()
        {
            // Note: If people manually change the formatting of dumps to list sectors twice, then two entries
            // for the same sector will be added to the list. The code should still be able to process it 
            //correctly, but if for some reason an error with multiple database entries occurs in the future
            // this may be the reason.

            List<Sector> list = new List<Sector>();

            using (StringReader sr = new StringReader(_data))
            {
                Sector sector = null;
                Planet planet = null;
                Moon moon = null;

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "Moon	Moon Product	Quantity	Ore TypeID	SolarSystemID	PlanetID	MoonID") continue;

                    if (line.Substring(0, 1) != "\t")
                    {
                        string[] parts = line.Split(null);
                        string sectorName = parts[0];
                        int planetNumber = RomanToInteger(parts[1]);
                        int moonNumber = Int32.Parse(parts[4]);

                        if (sector == null)
                        {
                            sector = new Sector(sectorName);
                        }
                        else if (sector.Name != sectorName)
                        {
                            planet = null;
                            moon = null;

                            list.Add(sector);
                            sector = new Sector(sectorName);
                        }

                        if (planet == null)
                        {
                            planet = new Planet(planetNumber);
                            sector.Planets[planetNumber] = planet;
                        }
                        else if (planet.Number != planetNumber)
                        {
                            moon = null;

                            if (sector.Planets.ContainsKey(planetNumber))
                            {
                                planet = sector.Planets[planetNumber];
                            }
                            else
                            {
                                planet = new Planet(planetNumber);
                                sector.Planets[planetNumber] = planet;
                            }
                        }

                        if (moon == null)
                        {
                            moon = new Moon(moonNumber);
                            planet.Moons[moonNumber] = moon;
                        }
                        else if (moon.Number != moonNumber)
                        {
                            if (planet.Moons.ContainsKey(moonNumber))
                            {
                                moon = planet.Moons[moonNumber];
                            }
                            else
                            {
                                moon = new Moon(moonNumber);
                                planet.Moons[moonNumber] = moon;
                            }
                        }
                    }
                    else
                    {
                        // malformed data dump
                        if (moon == null)
                            return null;

                        string[] parts = line.Split("\t");
                        string oreType = parts[1]; // parts[0] is nothing
                        double percentage = double.Parse(parts[2]);

                        moon.AddOre(new OreType(oreType, ""), percentage);
                    }
                }

                if (sector != null)
                {
                    list.Add(sector);
                }
            }

            return list;
        }
    }
}
