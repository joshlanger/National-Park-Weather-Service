using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;

namespace Capstone.Web.Tests
{
    [TestClass]
    public class CapstoneWebDAOTests
    {
        protected string ConnectionString = "Server=.\\SQLEXPRESS;Database=NPGeek;Trusted_Connection=True";
        private TransactionScope transaction;
        protected string NewParkId { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            transaction = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string cmd = "DELETE FROM weather; DELETE FROM survey_result; DELETE FROM park;";
                SqlCommand command = new SqlCommand(cmd, conn);
                command.ExecuteNonQuery();
                cmd = "INSERT INTO park VALUES('ARCH', 'Cuyahoga Valley National Park', 'Ohio', 32832, 696, 125, 0, 'Woodland', 2000, 2189849, 'This place is great!', 'Joseph Laneve', 'A lot of sandstone arches here', 0, 390);select scope_identity();";
                command = new SqlCommand(cmd, conn);
                NewParkId = Convert.ToString(command.ExecuteScalar());
                cmd = $"INSERT INTO survey_result VALUES({NewParkId}, 'joseph.c.laneve@gmail.com', 'PA', 'Active');";
                command = new SqlCommand(cmd, conn);
                //cmd = "SELECT COUNT(*) as votes, survey_result.parkCode, parkName FROM survey_result JOIN park on survey_result.parkCode = park.parkCode GROUP BY parkName, survey_result.parkCode ORDER BY votes DESC, survey_result.parkCode;";
                //command = new SqlCommand(cmd, conn);
                cmd = $"INSERT INTO weather VALUES({NewParkId}, 6, 43, 73, 'partly cloudy');";
                command = new SqlCommand(cmd, conn);
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            transaction.Dispose();
        }

        protected int GetRowCount(string table)
        {
            int count;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", conn);
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return count;
        }
    }
}
