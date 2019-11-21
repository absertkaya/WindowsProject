using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Model
{
    public class Weather
    {
        public string WeatherType { get; set; }
        public string Description { get; set; }
        public decimal Temp { get; set; }
        public int Humidity { get; set; }
        public decimal WindSpeed { get; set; }
    }
}
