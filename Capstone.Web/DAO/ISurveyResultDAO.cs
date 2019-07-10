using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO
{
    public interface ISurveyResultDAO
    {
        IList<SurveyResult> GetSurveys();
        void AddSurvey(SurveyResult survey);
    }
}
