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
        protected string ConnectionString = "Server=.SQLEXPRESS;Database=NPGeek;Trusted_Connection=True";
        private TransactionScope transaction;

        [TestInitialize]
        public void Initialize()
        {
            transaction = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string cmd = "DELTE FROM weather, DELETE FROM survey_result, DELETE FROM park;";
                SqlCommand command = new SqlCommand(cmd, conn);
                command.ExecuteNonQuery();
                cmd = "INSERT INTO park VALUES('ARCH', 'Arches National Park', 'Utah', 47389, 7000, 200, 100, 'Desert', 1929, 1284767, 'This place is great!', 'Joseph Laneve', 'A lot of sandstone arches here', 10, 500;";
                command = new SqlCommand(cmd, conn);
                cmd = "INSERT INTO survey_result VALUES('ARCH', 'joseph.c.laneve@gmail.com', 'Pennsylvania', 'Active';";
                command = new SqlCommand(cmd, conn);
                cmd = "INSERT INTO weather VALUES('ARCH', 1, 43, 73, 'partly cloudy';";
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
