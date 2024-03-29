﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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

        //this method is called to load the details page by default
        [HttpGet]
        public IActionResult Detail(string id)
        {
            ParkDetails currentDetails = new ParkDetails();
            currentDetails.IsCelsius = AccessTemperatureDetails();
            //uses the id from the park page that the user selected to get info on the specific park, putting it into a list with only one index.
            IList<Park> SingleParkList = parkDAO.GetSelectedPark(id);
            Park SelectedPark = new Park();
            //gets the park out of the list and assigns it to a single park.
            SelectedPark = SingleParkList[0];
            //takes the single park and assigns it to the parkdetails object to be passed to the detail page
            currentDetails.DetailPark = SelectedPark;
            currentDetails.FahrenheitWeather = weatherDAO.GetWeather(id);
            currentDetails.AllWeather = weatherDAO.GetWeather(id);
            if (currentDetails.IsCelsius == true)
            {
                currentDetails.AllWeather = currentDetails.ConvertTemp(currentDetails.AllWeather, currentDetails.IsCelsius);
            }
            return View(currentDetails);
        }

        //this method only gets called if the user changes their temperature preference
        [HttpPost]
        public IActionResult Detail(string id, ParkDetails currentDetails)
        {
            //compares the current user choice agains the user choice saved in the session, updating it if necessary
            bool currentTempCondition = CompareTemperatureDetails(currentDetails.IsCelsius);
            //assigns the value from the session to the model.
            currentDetails.IsCelsius = currentTempCondition;

            //uses the id from the park page that the user selected to get info on the specific park, putting it into a list with only one index.
            IList<Park> SingleParkList = parkDAO.GetSelectedPark(id);
            Park SelectedPark = new Park();
            //gets the park out of the list and assigns it to a single park.
            SelectedPark = SingleParkList[0];
            //takes the single park and assigns it to the parkdetails object to be passed to the detail page
            currentDetails.DetailPark = SelectedPark;
      
            //get two lists of weather details; a fahrenheit one for making comparisons for giving weather advice, and a second for displaying user temp preference.
            currentDetails.FahrenheitWeather = weatherDAO.GetWeather(id);
            currentDetails.AllWeather = weatherDAO.GetWeather(id);
            //if the condition of the session/model is false, run the temperatures in the list through a converter
            if (currentTempCondition == true)
            {
                currentDetails.AllWeather = currentDetails.ConvertTemp(currentDetails.AllWeather, currentTempCondition);
            }

            return View(currentDetails);
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
        
        //accesses the session to compare current temperature preference vs. a saved preference.
        //updates the session to reflect a new preference if necessary.
        private bool CompareTemperatureDetails(bool isFahrenheit)
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

        //helper method that saves temperature preference bool to the session.
        private void SaveTemperatureDetails (bool temp)
        {
            string temperature_string = JsonConvert.SerializeObject(temp);
            HttpContext.Session.SetString("Temperature", temperature_string);
        }

        //reads the temperature preference from the session
        private bool AccessTemperatureDetails()
        {
            bool sessionState = false;


            if (HttpContext.Session.GetString("Temperature") != null)
            {

                string temperature_string = HttpContext.Session.GetString("Temperature");
                sessionState = JsonConvert.DeserializeObject<bool>(temperature_string);
            }
            return sessionState;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
