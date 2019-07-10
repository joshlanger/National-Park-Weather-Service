using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAO;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Instaniate the Park DAO
        /// </summary>
        private IParkDAO parkDAO;
        public HomeController(IParkDAO parkDAO)
        {
            this.parkDAO = parkDAO;
        }

        /// <summary>
        /// Controls the Home Page view of the parks
        /// </summary>
        /// <param name="park">Passing in a paramter of Park</param>
        /// <returns>Returns a view of the Home Page of parks</returns>
        public IActionResult Park(Park park)
        {
            IList<Park> output = parkDAO.GetParks();
            park.AllParks = output;
            return View(park);
        }

        public IActionResult Detail(string id)
        {
            IList<Park> SingleParkList = parkDAO.GetSelectedPark(id);
            Park SelectedPark = new Park();
            SelectedPark = SingleParkList[0];
            return View(SelectedPark);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
