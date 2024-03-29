﻿using System;
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
        Survey survey = new Survey();
        private ISurveyDAO surveyDAO;
        private IParkDAO parkDAO;
        public SurveyController(ISurveyDAO surveyDAO, IParkDAO parkDAO)
        {
            this.surveyDAO = surveyDAO;
            this.parkDAO = parkDAO;
        }

        [HttpGet]
        public IActionResult Input()
        {
            survey.ParkCodes = surveyDAO.ParkCodes();
            return View("Input", survey);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Input(Survey survey)
        {
            if(!ModelState.IsValid)
            {
                survey.ParkCodes = surveyDAO.ParkCodes();
                return View("Input", survey);
            }
            surveyDAO.AddSurvey(survey);
            //CHANGED THE REDIRECT FROM ("RESULTS", "INPUT") TO "RESULTS"
            return RedirectToAction("Results");
        }

        [HttpGet]
        public IActionResult Results()
        {
            IList<Survey> SurveyResults = surveyDAO.GetSurveyResults();
            ResultsView surveyModel = new ResultsView()
            {
                AllSurveys = surveyDAO.GetSurveyResults(),
                AllParks = parkDAO.GetParks(),
            };
            return View(surveyModel);
            //REMOVED "RESULTS" FROM THE RETURN VIEW STATEMENT
        }
    }
}