using Capstone.Web.DAO;
using Capstone.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Web.Tests
{
    [TestClass]
    public class ParkSqlDAOTests : CapstoneWebDAOTests
    {
        private ParkSqlDAO parkDAO { get; set; }

        [TestMethod]
        public void GetParks_Should_Return_One_Park()
        {
            parkDAO = new ParkSqlDAO(ConnectionString);
            IList<Park> AllParks = parkDAO.GetParks();
            Assert.AreEqual(1, AllParks.Count);
        }
    }
}
