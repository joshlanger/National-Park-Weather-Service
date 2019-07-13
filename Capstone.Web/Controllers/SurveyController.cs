using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAO;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private ISurveyDAO surveyResultDAO;
        private IParkDAO parkDAO;
        public SurveyController(ISurveyDAO surveyResultDAO, IParkDAO parkDAO)
        {
            this.surveyResultDAO = surveyResultDAO;
            this.parkDAO = parkDAO;
        }

        [HttpGet]
        public IActionResult Input()
        {
            Survey survey = new Survey();
            survey.ParkCodes = ParkCodes;
            survey.SelectActivityLevel = ActivityLevels;
            return View("Input", survey);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Input(Survey survey)
        {
            if(!ModelState.IsValid)
            {
                survey.ParkCodes = ParkCodes;
                survey.SelectActivityLevel = ActivityLevels;
                return View("Input", survey);
            }
            surveyResultDAO.AddSurvey(survey);
            return RedirectToAction("Results", "Input");
        }

        [HttpGet]
        public IActionResult Results()
        {
            IList<Survey> SurveyResults = surveyResultDAO.GetSurveyResults();
            ResultsView surveyModel = new ResultsView()
            {
                SurveyResults = SurveyResults,
                Parks = parkDAO.GetParks(),
            };
            
            return View("Results", surveyModel);
        }

        List<SelectListItem> ParkCodes = new List<SelectListItem>()
        {
            new SelectListItem { Text = "Cuyahoga Valley National Park", Value = "CVNP" },
            new SelectListItem { Text = "Everglades National Park", Value = "ENP" },
            new SelectListItem { Text = "Grand Canyon National Park", Value = "GCNP" },
            new SelectListItem { Text = "Glacier National Park", Value = "GNP"},
            new SelectListItem { Text = "Great Smoky Mountains National Park", Value = "GSMNP" },
            new SelectListItem { Text = "Grand Teton National Park", Value = "GTNP" },
            new SelectListItem { Text = "Mount Rainier National Park", Value = "MRNP" },
            new SelectListItem { Text = "Rocky Mountain National Park", Value = "RMNP" },
            new SelectListItem { Text = "Yellowstone National Park", Value = "YNP" },
            new SelectListItem { Text = "Yosemite National Park", Value = "YNP2" }
        };

        List<SelectListItem> ActivityLevels = new List<SelectListItem>()
        {
            new SelectListItem { Text = "Inactive", Value = "Inactive" },
            new SelectListItem { Text = "Sedentary", Value = "Sedentary" },
            new SelectListItem { Text = "Active", Value = "Active" },
            new SelectListItem { Text = "Extremely Active", Value = "Extremely Active}" }
        };
    }
}