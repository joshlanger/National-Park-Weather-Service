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
        public IList<Weather> FahrenheitWeather { get; set; }
        public bool IsFahrenheit { get; set; }
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

        public IList<Weather> ConvertTemp (IList<Weather> allWeather, bool isFahrenheit)
        {
            double toCelcius = 5.0 / 9;
            double toFahrenheit = 9.0 / 5;
            
            if(isFahrenheit)
            {
                foreach(var day in allWeather)
                {
                    day.High = Math.Round((day.High - 32) * toCelcius);
                    day.Low = Math.Round((day.Low - 32) * toCelcius);
                }
                return allWeather;
            }
            else
            {
                foreach(var day in allWeather)
                {
                    day.High = Math.Round((day.High * toFahrenheit) + 32);
                    day.Low = Math.Round((day.Low * toFahrenheit) + 32);
                }
                return allWeather;
            }
            
        }

        //this method needs to set the isFahrenheit property based on the action from the toggle button
        public void SwitchTemperatures()
        {
           
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
