﻿using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;


namespace Capstone.Web.DAO
{
    public class ParkSqlDAO : IParkDAO
    {
        /// <summary>
        /// Instantiating the connection string for the SQL database
        /// </summary>
        private string connectionString;
        public ParkSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// A method to build a list of Park objects from the SQL database using Dapper
        /// </summary>
        /// <returns>A list of Park objects</returns>
        public IList<Park> GetParks()
        {
            IList<Park> AllParks = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string cmd = "SELECT * FROM park";
                    AllParks = conn.Query<Park>(cmd).ToList();
                }
            }
            catch(SqlException)
            {
                throw;
            }
            return AllParks;
        }

        /// <summary>
        /// A method to call on a specific park for the detail page using Dapper
        /// </summary>
        /// <param name="parkCode"></param>
        /// <returns></returns>
        public IList<Park> GetSelectedPark (string parkCode)
        {
            IList<Park> SingleParkList = new List<Park>();
            Park SelectedPark = new Park();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string cmd = "select * from park where parkcode = @parkCode;";
                    SingleParkList = conn.Query<Park>(cmd, new {parkCode}).ToList();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return SingleParkList;
        }
    }
}
