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
    public class SurveyResultSqlDAO : ISurveyResultDAO
    {
        /// <summary>
        /// Instantiating the connection string for the SQL database
        /// </summary>
        private string connectionString;
        public SurveyResultSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// A method to build a list of Survey Result objects from the SQL database using Dapper
        /// </summary>
        /// <returns>A list of Survey Result objects</returns>
        public IDictionary<string, int> GetSurveys()
        {
            IDictionary<string, int> AllSurveys = new Dictionary<string, int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string cmd = "SELECT COUNT(*) as votes, parkCode FROM survey_result GROUP BY parkCode ORDER BY votes DESC, parkCode;";
                    AllSurveys = conn.Query(cmd).ToDictionary(
                        row => (string)row.parkCode,
                        row => (int)row.votes);
                }
            }
            catch(SqlException)
            {
                throw;
            }
            return AllSurveys;
            
        }

        public IList<SurveyResult> GetNames()
        {
            IList<SurveyResult> Names = new List<SurveyResult>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string cmd = "SELECT parkCode, parkName FROM park;";
                    Names = conn.Query<SurveyResult>(cmd).ToList();
                }
            }
            catch(SqlException)
            {
                throw;
            }
            return Names;
        }

        /// <summary>
        /// A method to post a survey to the Survey Results table using Dapper
        /// </summary>
        /// <param name="survey">Passing in the of a Survey</param>
        public void AddSurvey(SurveyResult survey)
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
                    
                    ////string cmd = ("INSERT INTO survey_result VALUES (parkCode = @ParkCode, emailAddress = @EmailAddress, state = @State, activityLevel = @ActivityLevel)");
                    ////cmd = conn.Execute(cmd, new { survey.ParkCode, survey.EmailAddress, survey.State, survey.ActivityLevel }).ToString();
                    //SqlCommand cmd = new SqlCommand("INSERT INTO survey_result VALUES(@parkCode, @emailAddress, @state, @activityLevel);", conn);
                    //cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    //cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    //cmd.Parameters.AddWithValue("@state", survey.State);
                    //cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    //cmd.ExecuteNonQuery();
                }
            }
            catch(SqlException)
            {
                throw;
            }
        }
    }
}
