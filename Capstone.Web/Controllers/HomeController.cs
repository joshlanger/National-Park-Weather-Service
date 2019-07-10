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
        private IWeatherDAO weatherDAO;
        private ISurveyResultDAO surveyResultDAO;
        public HomeController(IParkDAO parkDAO, IWeatherDAO weatherDAO, ISurveyResultDAO surveyResultDAO)
        {
            this.surveyResultDAO = surveyResultDAO;
            this.weatherDAO = weatherDAO;
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
        
        //is it ok to have all this logic in the controller?  some of it could be moved.
        public IActionResult Detail(string id)
        {
            IList<Park> SingleParkList = parkDAO.GetSelectedPark(id);
            Park SelectedPark = new Park();
            SelectedPark = SingleParkList[0];
            ParkDetails CurrentDetails = new ParkDetails();
            CurrentDetails.DetailPark = SelectedPark;
            IList<Weather> CurrentPark = weatherDAO.GetWeather(id);
            CurrentDetails.ParkWeather = CurrentPark[0];
            return View(CurrentDetails);
        }

        [HttpGet]
        public IActionResult Survey()
        {
            SurveyResult surveyResult = new SurveyResult();
            surveyResult.Names = surveyResultDAO.GetParkNames();
            return View(surveyResult);
        }

        [HttpPost]
        public IActionResult Suvey(SurveyResult survey)
        {
            surveyResultDAO.AddSurvey(survey);
            return RedirectToAction("Survey", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
