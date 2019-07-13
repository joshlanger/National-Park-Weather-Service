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
                    string cmd = "SELECT COUNT(*) as votes, parkCode FROM survey_result GROUP BY parkCode ORDER BY votes DESC, parkCode;";
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
                    string cmd = "INSERT INTO survey_result VALUES (@parkCode, @emailAddress, @state, @activityLevel)";
                    conn.Query(cmd, new
                    {
                        parkCode = survey.ParkCode,
                        emailAddress = survey.EmailAddress,
                        state = survey.State,
                        activityLevel = survey.ActivityLevel
                    });
                    
                }
            }
            catch(SqlException)
            {
                throw;
            }
        }
    }
}
