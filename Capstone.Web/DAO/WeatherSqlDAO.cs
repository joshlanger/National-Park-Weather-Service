using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;
using Dapper;

namespace Capstone.Web.DAO
{
    public class WeatherSqlDAO : IWeatherDAO
    {
        /// <summary>
        /// Instantiating the connection string for the SQL database
        /// </summary>
        private string connectionString;
        public WeatherSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// A method to build a list of Park objects from the SQL database using Dapper
        /// </summary>
        /// <returns>A list of Park objects</returns>
        public IList<Weather> GetWeather()
        {
            IList<Weather> AllWeather = new List<Weather>();
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string cmd = "SELECT * FROM weather";
                        AllWeather = conn.Query<Weather>(cmd).ToList();
                    }
                }
                catch(SqlException)
                {
                    throw;
                }
                return AllWeather;
            }
        }
    }
}
