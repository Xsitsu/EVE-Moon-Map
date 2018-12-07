using EVE_Moon_Map.Classes;
using EVE_Moon_Map.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EVE_Moon_Map.Repository
{
    public class WorldDBRepository : IWorldRepository
    {
        protected string GetConnectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<Startup>();
            var configuration = builder.Build();
            string connectionstring = configuration.GetConnectionString("EVE_Moon_MapContextConnection");
            return connectionstring;
        }

        // System Functions
        public void AddSystem(string system)
        {
            if (SystemExists(system))
            {
                return;
            }

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Systems_Insert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@SystemName", system);

                    command.ExecuteNonQuery();
                }
            }
        }

        public int GetSystemId(string system)
        {
            int systemId = -1;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Systems_GetByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@SystemName", system);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            systemId = (int)reader["SystemId"];
                        }
                    }
                }
            }

            return systemId;
        }

        public bool SystemExists(string system)
        {
            return (GetSystemId(system) != -1);
        }

        // Planet Functions
        public void AddPlanet(int planetNumber, int systemId)
        {
            if (PlanetExists(planetNumber, systemId))
            {
                return;
            }

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Planets_Insert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@SystemId", systemId);
                    command.Parameters.AddWithValue("@PlanetNumber", planetNumber);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<int> GetPlanetsInSystem(int systemId)
        {
            List<int> list = new List<int>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Planets_GetListInSystem", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@SystemId", systemId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add((int)reader["PlanetNumber"]);
                        }
                    }
                }
            }

            return list;
        }

        public int GetPlanetId(int planetNumber, int systemId)
        {
            int planetId = -1;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Planets_GetByNumber", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@SystemId", systemId);
                    command.Parameters.AddWithValue("@PlanetNumber", planetNumber);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            planetId = (int)reader["PlanetId"];
                        }
                    }
                }
            }

            return planetId;
        }

        public bool PlanetExists(int planetNumber, int systemId)
        {
            return (GetPlanetId(planetNumber, systemId) != -1);
        }

        // Moon Functions
        public void AddMoon(int moonNumber, int planetId)
        {
            if (MoonExists(moonNumber, planetId))
            {
                return;
            }

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Moons_Insert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@PlanetId", planetId);
                    command.Parameters.AddWithValue("@MoonNumber", moonNumber);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<int> GetMoonsInPlanet(int planetId)
        {
            List<int> list = new List<int>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Moons_GetListInPlanet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@PlanetId", planetId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add((int)reader["MoonNumber"]);
                        }
                    }
                }
            }

            return list;
        }

        public int GetMoonId(int moonNumber, int planetId)
        {
            int moonId = -1;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Moons_GetByNumber", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@PlanetId", planetId);
                    command.Parameters.AddWithValue("@MoonNumber", moonNumber);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            moonId = (int)reader["MoonId"];
                        }
                    }
                }
            }

            return moonId;
        }

        public bool MoonExists(int moonNumber, int planetId)
        {
            return (GetMoonId(moonNumber, planetId) != -1);
        }

        // MoonOre Functions
        public void AddMoonOre(int moonId, int oreId, int percentage)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("MoonOres_Insert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@MoonId", moonId);
                    command.Parameters.AddWithValue("@OreId", oreId);
                    command.Parameters.AddWithValue("@Percentage", percentage);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMoonOre(int moonId)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("MoonOres_DeleteAllInMoon", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@MoonId", moonId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Dictionary<int, int> GetOresInMoon(int moonId)
        {
            Dictionary<int, int> list = new Dictionary<int, int>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("MoonOres_GetListInMoon", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@MoonId", moonId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int oreId = (int)reader["OreId"];
                            int percentage = (int)reader["Percentage"];

                            list[oreId] = percentage;
                        }
                    }
                }
            }

            return list;
        }

        // Search Functions
        public List<Sector> Search(MoonSearchQuery query)
        {
            Dictionary<string, Sector> result = new Dictionary<string, Sector>();
            List<Sector> list = new List<Sector>();

            int switch0 = 1;
            int switch1 = 1;
            int switch2 = 1;
            int switch3 = 1;
            string systemName = "";
            string oreName = "";
            int oreClassId = 0;
            int percentage = 0;

            if (query.SystemName != null && query.SystemName != "")
            {
                switch0 = 0;
                systemName = query.SystemName;
            }
            if (query.OreName != "Any")
            {
                switch1 = 0;
                oreName = query.OreName;
            }
            if (query.OreClassId > 0)
            {
                switch2 = 0;
                oreClassId = query.OreClassId;
            }
            if (query.Percentage > 0)
            {
                switch3 = 0;
                percentage = query.Percentage;
            }

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Search", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@Switch0", switch0);
                    command.Parameters.AddWithValue("@Switch1", switch1);
                    command.Parameters.AddWithValue("@Switch2", switch2);
                    command.Parameters.AddWithValue("@Switch3", switch3);
                    command.Parameters.AddWithValue("@System", systemName);
                    command.Parameters.AddWithValue("@Ore", oreName);
                    command.Parameters.AddWithValue("@OreClassId", oreClassId);
                    command.Parameters.AddWithValue("@Percentage", percentage);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sectorName = reader["System"].ToString();
                            int planetNumber = (int)reader["Planet"];
                            int moonNumber = (int)reader["Moon"];
                            string oreType = reader["Ore"].ToString();
                            int percent = (int)reader["Percentage"];
                            string oreClass = reader["OreClass"].ToString();

                            Sector sector = null;
                            Planet planet = null;
                            Moon moon = null;

                            if (result.ContainsKey(sectorName))
                            {
                                sector = result[sectorName];
                            }
                            else
                            {
                                sector = new Sector(sectorName);
                                result[sectorName] = sector;
                            }

                            if (sector.Planets.ContainsKey(planetNumber))
                            {
                                planet = sector.Planets[planetNumber];
                            }
                            else
                            {
                                planet = new Planet(planetNumber);
                                sector.Planets[planetNumber] = planet;
                            }

                            if (planet.Moons.ContainsKey(moonNumber))
                            {
                                moon = planet.Moons[moonNumber];
                            }
                            else
                            {
                                moon = new Moon(moonNumber);
                                planet.Moons[moonNumber] = moon;
                            }

                            moon.AddOre(new OreType(oreType, oreClass), ((double)percent) / 100);
                        }
                    }
                }
            }

            foreach (var item in result)
            {
                list.Add(item.Value);
            }

            return list;
        }
    }
}
