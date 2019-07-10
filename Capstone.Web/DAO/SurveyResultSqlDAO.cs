using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

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
                    string cmd = "SELECT * FROM survey_result";
                    AllSurveys = conn.Query<SurveyResult>(cmd).ToList();
                }
            }
            catch(SqlException)
            {
                throw;
            }
            return AllSurveys;
        }

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
    }
}
