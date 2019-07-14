using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Capstone.Web.DAO
{
    public class SurveySqlDAO : ISurveyDAO
    {
        /// <summary>
        /// Instantiating the connection string for the SQL database
        /// </summary>
        private string connectionString;
        public SurveySqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// A method to build a list of Survey Result objects from the SQL database using Dapper
        /// </summary>
        /// <returns>A list of Survey Result objects</returns>
        public IList<Survey> GetSurveyResults()
        {
            IList<Survey> AllSurveys = new List<Survey>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string cmd = "SELECT COUNT(*) as votes, survey_result.parkCode, parkName FROM survey_result JOIN park on survey_result.parkCode = park.parkCode GROUP BY parkName, survey_result.parkCode ORDER BY votes DESC, survey_result.parkCode;";
                    AllSurveys = conn.Query<Survey>(cmd).ToList();
                }
            }
            catch(SqlException)
            {
                throw;
            }
            return AllSurveys;
        }

        /// <summary>
        /// A method to post a survey to the Survey Results table using Dapper
        /// </summary>
        /// <param name="survey">Passing in the of a Survey</param>
        public void AddSurvey(Survey survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO survey_result VALUES (@parkCode, @emailAddress, @state, @activityLevel);", conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    cmd.ExecuteNonQuery();
                }
            }
            catch(SqlException)
            {
                throw;
            }
        }

        public IList<SelectListItem> ParkCodes()
        {
            IList<SelectListItem> ParkCodes = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT parkCode, parkName from park;", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    ParkCodes.Add(new SelectListItem() { Text = Convert.ToString(reader["parkName"]), Value = Convert.ToString(reader["parkCode"]) });
                }
            }
            return ParkCodes;
        }
    }
}
