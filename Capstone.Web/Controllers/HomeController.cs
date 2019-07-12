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

        ParkDetails temp = new ParkDetails();
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

        //TODO is it ok to have all this logic in the controller?  some of it could be moved.
        /// <summary>
        /// Controls the Detail Page view of each park
        /// </summary>
        /// <param name="id">Passing in a parameter of Park Code</param>
        /// <returns>Returns a view of the Detail Page of the park that matched the parameter Park Code</returns>
        public IActionResult Detail(string id, ParkDetails currentDetails)
        {
            bool myonoffswitch = currentDetails.IsFahrenheit;
            //uses the id from the park page that the user selected to get info on the specific park, putting it into a list with only one index.
            IList<Park> SingleParkList = parkDAO.GetSelectedPark(id);
            Park SelectedPark = new Park();
            //gets the park out of the list and assigns it to a single park.
            SelectedPark = SingleParkList[0];
            //takes the single park and assigns it to the parkdetails object to be passed to the detail page
            currentDetails.DetailPark = SelectedPark;
            //this takes the current condition of the isFahrenheit property in the parkdetails model and checks it against the session.
            bool currentTempCondition = GetTemperatureDetails(currentDetails.IsFahrenheit);
            //checks the model against the session
            GetTemperatureDetails(currentTempCondition);
            //get two lists of weather details; a fahrenheit one for making comparisons for giving weather advice, and a second for displaying user temp preference.
            currentDetails.FahrenheitWeather = weatherDAO.GetWeather(id);
            currentDetails.AllWeather = weatherDAO.GetWeather(id);
            //if the condition of the session/model is false, run the temperatures in the list through a converter
            if(currentTempCondition == false)
            {
                currentDetails.AllWeather = currentDetails.ConvertTemp(currentDetails.AllWeather, currentTempCondition);
            }
            
            return View(currentDetails);
        }

        /// <summary>
        /// Controls the Survey Page view
        /// </summary>
        /// <returns>Returns a view of the Survey Page</returns>
        [HttpGet]
        public IActionResult Survey()
        {
            SurveyResult surveyResult = new SurveyResult();
            surveyResult.Names = surveyResultDAO.GetParkNames();
            return View(surveyResult);
        }

        /// <summary>
        /// Controls the posting of the Survey back to the database
        /// </summary>
        /// <param name="survey">Passing in the parameter of a survey</param>
        /// <returns>Redirects to the Favorite Park survey result Page and updates the database</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Survey(SurveyResult survey)
        {
            if(!ModelState.IsValid)
            {
                return View(survey);
            }
            surveyResultDAO.AddSurvey(survey);
            return RedirectToAction("Favorite", "Home");
        }

        /// <summary>
        /// Controls the Favorite Park Page view
        /// </summary>
        /// <returns>Returns a view of the Favorite Park Page</returns>
        [HttpGet]
        public IActionResult Favorite()
        {
            return View();
        }

        private bool GetTemperatureDetails(bool isFahrenheit)
        {

            bool sessionState = false;
            

            if(HttpContext.Session.GetString("Temperature") != null)
            {
               
                string temperature_string = HttpContext.Session.GetString("Temperature");
                sessionState = JsonConvert.DeserializeObject<bool>(temperature_string); 
                if(sessionState != isFahrenheit)
                {
                    SaveTemperatureDetails(isFahrenheit);
                }
                else
                {
                    return isFahrenheit;
                }
            }
            else
            {
                SaveTemperatureDetails(isFahrenheit);
            }
            return isFahrenheit;
        }

        private void SaveTemperatureDetails (bool temp)
        {
            string temperature_string = JsonConvert.SerializeObject(temp);
            HttpContext.Session.SetString("Temperature", temperature_string);
        }

        //we don't think we need this method to accomplish what we are trying to do in the session, though one was present in the lecture code.
        //public ActionResult SwitchTemperature (string id, bool isFahrenheit)
        //{

        //    temp.AllWeather = weatherDAO.GetWeather(id);

        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
