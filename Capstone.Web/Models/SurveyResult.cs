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
        /// Email address of the survey taker
        /// </summary>
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// State where the survey taker lives
        /// </summary>
        [Display(Name ="State")]
        public string State { get; set; }

        /// <summary>
        /// Activity level of the survey taker
        /// </summary>
        [Display(Name ="Activity Level")]
        public string ActivityLevel { get; set; }

        /// <summary>
        /// List of survey results from the database
        /// </summary>
        public IList<SurveyResult> AllSurveys { get; set; }
    }
}
