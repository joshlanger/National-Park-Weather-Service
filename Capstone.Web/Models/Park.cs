using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Park
    {
        /// <summary>
        /// Alpha Park Identifier
        /// </summary>
        [Display(Name = "Park Code")]
        public string ParkCode { get; set; }

        /// <summary>
        /// Name of the park
        /// </summary>
        [Display(Name = "Name")]
        public string ParkName { get; set; }

        /// <summary>
        /// State where the park is located
        /// </summary>
        [Display(Name = "State")]
        public string State { get; set; }

        /// <summary>
        /// Park acreage, in int form
        /// </summary>
        [Display(Name = "Acreage")]
        public int Acreage { get; set; }

        /// <summary>
        /// Park elevation, in int form representing feet
        /// </summary>
        [Display(Name = "Elevation")]
        public int ElevationInFeet { get; set; }

        /// <summary>
        /// Number of miles of trails, in double form
        /// </summary>
        [Display(Name = "Miles of Trail")]
        public double MilesOfTrail { get; set; }

        /// <summary>
        /// Total campsites per park
        /// </summary>
        [Display(Name = "Number of Campsites")]
        public int NumberOfCampsites { get; set; }

        /// <summary>
        /// Description of the park climate
        /// </summary>
        [Display(Name = "Climate")]
        public string Climate { get; set; }

        /// <summary>
        /// Year park was established, in int form
        /// </summary>
        [Display(Name = "Year Founded")]
        public int YearFounded { get; set; }

        /// <summary>
        /// Number of visitors to the park each year
        /// </summary>
        [Display(Name = "Annual Visitor Count")]
        public int AnnualVisitorCount { get; set; }

        /// <summary>
        /// Quote about the park
        /// </summary>
        [Display(Name = "Inspirational Quote")]
        public string InspirationalQuote { get; set; }

        /// <summary>
        /// Source of the inspirational quote
        /// </summary>
        [Display(Name = "Source")]
        public string InspirationalQuoteSource { get; set; }

        /// <summary>
        /// Description of the park
        /// </summary>
        [Display(Name = "Park Description")]
        public string ParkDescription { get; set; }

        /// <summary>
        /// Amount to enter a park, in int form
        /// </summary>
        [Display(Name = "Entry Fee")]
        public int EntryFee { get; set; }

        /// <summary>
        /// Number of different animal species
        /// </summary>
        [Display(Name = "Number of Animal Species")]
        public int NumberOfAnimalSpecies { get; set; }

        /// <summary>
        /// List of parks from the database
        /// </summary>
        public IList<Park> AllParks { get; set; }
    }
}
