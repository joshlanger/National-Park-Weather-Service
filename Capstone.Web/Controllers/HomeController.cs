using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {


        /// <summary>
        /// Instaniate the Park DAO
        /// </summary>
        private IParkDAO parkDAO;
        private IWeatherDAO weatherDAO;
        public HomeController(IParkDAO parkDAO, IWeatherDAO weatherDAO)
        {
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

        //TODO is it ok to have all this logic in the controller?  some of it could be moved.
        /// <summary>
        /// Controls the Detail Page view of each park
        /// </summary>
        /// <param name="id">Passing in a parameter of Park Code</param>
        /// <returns>Returns a view of the Detail Page of the park that matched the parameter Park Code</returns>
        public IActionResult Detail(string id, ParkDetails currentDetails)
        {
            IList<Park> SingleParkList = parkDAO.GetSelectedPark(id);
            Park SelectedPark = new Park();
            SelectedPark = SingleParkList[0];
            
            currentDetails.DetailPark = SelectedPark;
            //IList<Weather> CurrentPark = weatherDAO.GetWeather(id);
            currentDetails.AllWeather = weatherDAO.GetWeather(id);
            return View(currentDetails);
        }

        ///// <summary>
        ///// Controls the Survey Page view
        ///// </summary>
        ///// <returns>Returns a view of the Survey Page</returns>
        //[HttpGet]
        //public IActionResult Survey()
        //{
        //    SurveyResult surveyResult = new SurveyResult();
        //    surveyResult.Names = surveyResultDAO.GetParkNames();
        //    return View(surveyResult);
        //}

        ///// <summary>
        ///// Controls the posting of the Survey back to the database
        ///// </summary>
        ///// <param name="survey">Passing in the parameter of a survey</param>
        ///// <returns>Redirects to the Favorite Park survey result Page and updates the database</returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Survey(SurveyResult survey)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View(survey);
        //    }
        //    surveyResultDAO.AddSurvey(survey);
        //    return RedirectToAction("Favorite", "Home");
        //}

        /// <summary>
        /// Controls the Favorite Park Page view
        /// </summary>
        /// <returns>Returns a view of the Favorite Park Page</returns>
        [HttpGet]
        public IActionResult Favorite()
        {
            return View();
        }

        private ParkDetails GetTemperatureDetails()
        {
            ParkDetails temp = null;

            if(HttpContext.Session.GetString("Temperature")==null)
            {
                temp = new ParkDetails();
                SaveTemperatureDetails(temp);
            }
            else
            {
                string temperature_string = HttpContext.Session.GetString("Temperature");
                temp = JsonConvert.DeserializeObject<ParkDetails>(temperature_string);
            }
            return temp;
        }

        private void SaveTemperatureDetails (ParkDetails temp)
        {
            string temperature_string = JsonConvert.SerializeObject(temp);
            HttpContext.Session.SetString("Temperature", temperature_string);
        }

        //    //we left off here.  the idea is to use the converter within the controller
        //    public ActionResult SwitchTemperature (ParkDetails parkDetails, int temp)
        //    {
        //        parkDetails.AllWeather = weatherDAO.GetWeather()
        //    }

        //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //    public IActionResult Error()
        //    {
        //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //    }
    }
}
