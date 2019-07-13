using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO
{
    public interface ISurveyDAO
    {
        /// <summary>
        /// Inteface for the Park SQL DAO
        /// </summary>
        /// <returns></returns>
        IList<Survey> GetSurveyResults();
        void AddSurvey(Survey survey);
        IList<SelectListItem> ParkCodes();
    }
}
