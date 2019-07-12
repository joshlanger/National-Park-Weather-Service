using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        /// <summary>
        /// Alpha Park code from Park table
        /// </summary>
        [Display(Name = "")]
        public string ParkCode { get; set; }

        /// <summary>
        /// Number of the day in the forecast, in int form
        /// </summary>
        [Display(Name = "Day")]
        public int FiveDayForecastValue { get; set; }

        /// <summary>
        /// Low temperature, in int form and in Farenheit
        /// </summary>
        [Display(Name = "Low Temperature")]
        public double Low { get; set; }

        //note that you changed the temperatures from int to double

        /// <summary>
        /// High temperature, in int form and in Farenheit
        /// </summary>
        [Display(Name = "High Temperature")]
        public double High { get; set; }

        /// <summary>
        /// Description of the weather conditions
        /// </summary>
        [Display(Name = "Forecast")]
        public string Forecast { get; set; }

        /// <summary>
        /// List of five-day weather conditions for each park from the database
        /// </summary>
        

    }
}
