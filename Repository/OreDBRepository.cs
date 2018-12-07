using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EVE_Moon_Map.Classes;
using Microsoft.Extensions.Configuration;

namespace EVE_Moon_Map.Repository
{
    public class OreDBRepository : IOreRepository
    {
        protected string GetConnectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<Startup>();
            var configuration = builder.Build();
            string connectionstring = configuration.GetConnectionString("EVE_Moon_MapContextConnection");
            return connectionstring;
        }

        public Dictionary<int, OreType> GetOreList()
        {
            Dictionary<int, OreType> list = new Dictionary<int, OreType>();
            Dictionary<int, string> listClass = GetOreClassList();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Ore_GetList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int oreId = (int)reader["OreId"];
                            int oreClassId = (int)reader["OreClassId"];
                            string oreName = reader["OreName"].ToString();
                            string oreClassName = listClass[oreClassId];

                            list[oreId] = new OreType(oreName, oreClassName);
                        }
                    }
                }
            }

            return list;
        }
        public int GetOreId(string ore)
        {
            int oreId = -1;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Ore_GetByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@OreName", ore);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            oreId = (int)reader["OreId"];
                        }
                    }
                }
            }

            return oreId;
        }

        public Dictionary<int, string> GetOreClassList()
        {
            Dictionary<int, string> list = new Dictionary<int, string>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("OreClass_GetList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int oreClassId = (int)reader["OreClassId"];
                            string oreClassName = reader["OreClassName"].ToString();

                            list[oreClassId] = oreClassName;
                        }
                    }
                }
            }

            return list;
        }

        public int GetOreClassId(string oreClass)
        {
            int oreClassId = -1;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("OreClass_GetByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@OreClassName", oreClass);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            oreClassId = (int)reader["OreClassId"];
                        }
                    }
                }
            }

            return oreClassId;
        }
    }
}
