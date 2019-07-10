using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IList<SurveyResult> GetSurveys()
        {
            IList<SurveyResult> AllSurveys = new List<SurveyResult>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string cmd = "SELECT surveyId, survey_result.parkCode, parkName, emailAddress, survey_result.state, activityLevel FROM survey_result JOIN park ON survey_result.parkCode = park.parkCode;";
                    AllSurveys = conn.Query<SurveyResult>(cmd).ToList();
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
        public void AddSurvey(SurveyResult survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string cmd = ("INSERT INTO survey_result VALUES (parkCode = @ParkCode, emailAddress = @EmailAddress, state = @State, activityLevel = @ActivityLevel)");
                    cmd = conn.Execute(cmd, new { survey.ParkCode, survey.EmailAddress, survey.State, survey.ActivityLevel }).ToString();
                }
            }
            catch(SqlException)
            {
                throw;
            }
        }

        public IList<SelectListItem> GetParkNames()
        {
            IList<SelectListItem> Names = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT parkName FROM park;", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Names.Add(new SelectListItem() { Text = Convert.ToString(reader["parkName"]) });
                }
            }
            return Names;
        }
    }
}
