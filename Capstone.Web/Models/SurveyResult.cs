﻿using Capstone.Web.DAO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class SurveyResult
    {
        /// <summary>
        /// ID for survey
        /// </summary>
        [Display(Name = "Survey ID")]
        public int SurveyId { get; set; }

        /// <summary>
        /// Alpha Park code from Park table
        /// </summary>
        [Display(Name = "Park Code")]
        public string ParkCode { get; set; }

        /// <summary>
        /// Park name obtained through SQL table join
        /// </summary>
        [Display(Name ="Favorite National Park")]
        [Required]
        public string ParkName { get; set; }

        /// <summary>
        /// Email address of the survey taker
        /// </summary>
        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// State where the survey taker lives
        /// </summary>
        [Display(Name ="State")]
        [Required]
        public string State { get; set; }

        /// <summary>
        /// Activity level of the survey taker
        /// </summary>
        [Display(Name ="Activity Level")]
        //[Required]
        public string ActivityLevel { get; set; }

        /// <summary>
        /// List of survey results from the database
        /// </summary>
        public IList<SurveyResult> AllSurveys { get; set; }

        public IList<SurveyResult> Names { get; set; }

        public IList<SelectListItem> ParkNames = new List<SelectListItem>()
            {
            new SelectListItem { Text = "Inactive", Value = "Inactive" },
            new SelectListItem { Text = "Sedentary", Value = "Sedentary" },
            new SelectListItem { Text = "Active", Value = "Active" },
            new SelectListItem { Text = "Extremely Active", Value = "Extremely Active}" }
        };

        public IList<SelectListItem> ActivityLevels = new List<SelectListItem>()
        {
            new SelectListItem { Text = "Inactive", Value = "Inactive" },
            new SelectListItem { Text = "Sedentary", Value = "Sedentary" },
            new SelectListItem { Text = "Active", Value = "Active" },
            new SelectListItem { Text = "Extremely Active", Value = "Extremely Active}" }
        };
    }
}
