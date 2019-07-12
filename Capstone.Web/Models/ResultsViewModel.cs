using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ResultsViewModel
    {
        public IDictionary<string, int> SurveyResults { get; set; }
        public IList<Park> Parks { get; set; }
    }
}
