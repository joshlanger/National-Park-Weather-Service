using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO
{
    interface IWeatherDAO
    {
        /// <summary>
        /// Inteface for the Weather SQL DAO
        /// </summary>
        /// <returns></returns>
        IList<Weather> GetWeather();
    }
}
