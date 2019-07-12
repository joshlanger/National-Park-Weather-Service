using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAO;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private ISurveyResultDAO surveyResultDAO;
        private IParkDAO parkDAO;
        public SurveyController(ISurveyResultDAO surveyResultDAO, IParkDAO parkDAO)
        {
            this.surveyResultDAO = surveyResultDAO;
            this.parkDAO = parkDAO;
        }

        [HttpGet]
        public IActionResult Input()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Input(SurveyResult survey)
        {
            if(!ModelState.IsValid)
            {
                survey.ParkCode = survey.Names.ToString();
                survey.ActivityLevel = survey.ActivityLevels.ToString();
                return View("Input", survey);
            }
            surveyResultDAO.AddSurvey(survey);
            return RedirectToAction("Results", "Input");
        }

        [HttpGet]
        public IActionResult Results()
        {
            IDictionary<string, int> SurveyResults = surveyResultDAO.GetSurveys();
            ResultsViewModel surveyModel = new ResultsViewModel()
            {
                SurveyResults = SurveyResults,
                Parks = parkDAO.GetParks(),
            };
            
            return View("Results", surveyModel);
        }
    }
}