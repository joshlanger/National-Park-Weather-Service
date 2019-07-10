using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO
{
    public interface IParkDAO
    {
        /// <summary>
        /// Inteface for the Park SQL DAO
        /// </summary>
        /// <returns></returns>
        IList<Park> GetParks();
        IList<Park> GetSelectedPark(string parkCode);
    }
}
