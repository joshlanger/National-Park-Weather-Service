using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkDetails
    {
        public Park DetailPark { get; set; }
        public IList<Weather> AllWeather { get; set; }
        public Dictionary<string, string> WeatherAdvice = new Dictionary<string, string>()
        {
            {"snow", "Pack snow shoes." },
            {"rain", "Pack rain gear and wear waterproof shoes." },
            {"thunderstorms", "Seek shelter and avoid hiking on exposed ridges." },
            {"sunny", "Pack sunblock." },
            {"partly cloudy", "" },
            {"cloudy", "" }
            
        };

        public List<string> TemperatureAdvice = new List<string>()
        {
            {"Bring an extra gallon of water." },
            {"Wear breathable layers." },
            {"Danger! Exposure to temperatures this low can cause frost bite." }
        };

        public double ConvertTemp (double temperature)
        {
            double toCelcius = 5 / 9;
            double toFahrenheit = 9 / 5;
            //get value from session.  Add to if statement.  first is for converting to celcius.
            if(temperature == 0)
            {
                temperature = (temperature - 32) * toCelcius;
            }
            else
            {
                temperature = (temperature * toFahrenheit) + 32;
            }
            return Math.Round(temperature);
        }

        //public string TemperatureAdice
        //{
        //    get
        //    { if(this.AllWeather[0].High > 76)
        //        {
        //            return "Bring an extra gallon of water";
                          
        //        }
        //    if(AllWeather[0].Low < 20)
        //        {
        //            return "Danger! Exposure to temperatures this low can cause frost bite";
        //        }
        //    if((AllWeather[0].High - AllWeather[0].Low) > 20)
        //        {
        //            return "Wear breathable layers";
        //        }
        //        return "";
        //            }
        //}

        //public Dictionary<int, string> TemperatureAdvice = new Dictionary<int, string>()
        //{
        //    {76, "Bring an extra gallon of water" },
        //    {21, "Wear breathable layers" },
        //    {19, "Danger! Exposure to temperatures this low can cause frost bite" }
        //};
            
        

    }
}
