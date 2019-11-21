using FlightApp.Model;
using FlightApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Data
{
    public class FlightRepository
    {
        public static async Task<Flight> GetFlightDetailsAsync () {
            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", serv.Token));
            string json = await client.GetStringAsync(new Uri($"http://localhost:49681/api/Flight/detail/{serv.User.FlightId}"));
            Flight flight = JsonConvert.DeserializeObject<Flight>(json);
            return flight;
        }

        public static async Task<Weather> GetWeatherAsync(string city)
        {
            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(new Uri($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=8829a1e07622f065874303b2dcf9652d"));
            WeatherBulkDTO bulk = JsonConvert.DeserializeObject<WeatherBulkDTO>(json);
            Weather weather = new Weather()
            {
                WeatherType = bulk.Weather[0].Main,
                Description = bulk.Weather[0].Description,
                Temp = bulk.Main.Temp - 272.15M,
                Humidity = bulk.Main.Humidity
            };
            return weather;
        }

        public class WeatherBulkDTO
        {
            public MainDTO Main { get; set; }
            public IList<WeatherDTO> Weather { get; set; }
        }

        public class WeatherDTO
        {
            public string Main { get; set; }
            public string Description { get; set; }
        }

        public class MainDTO
        {
            public decimal Temp { get; set; }
            public int Humidity { get; set; }
        }
    }
}
