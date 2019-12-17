using FlightApp.Model;
using FlightApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace FlightApp.Data
{
    public class FlightRepository
    {
        public static async Task<Flight> GetFlightDetailsAsync () {
            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", serv.Token));
            string json = await client.GetStringAsync(new Uri($"https://flightappapi.azurewebsites.net/api/Flight/detail/{serv.User.FlightId}"));
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

        public static async Task<IList<Seat>> GetAllSeatsAsync()
        {
            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {serv.Token}");
            string json = await client.GetStringAsync(new Uri($"https://flightappapi.azurewebsites.net/api/Flight/{serv.User.FlightId}/get_seats"));
            IList<Seat> seats = JsonConvert.DeserializeObject<IList<Seat>>(json);
            return seats;
        }

        public static async Task<IList<Passenger>> GetFriendsAsync()
        {
            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {serv.Token}");
            string json = await client.GetStringAsync(new Uri($"http://flightappapi.azurewebsites.net/api/Flight/friends/{serv.User.FlightId}/"));
            IList<Passenger> friends = JsonConvert.DeserializeObject<IList<Passenger>>(json);
            return friends;
        }

        public static async Task<bool> PostMessage(Passenger friend, string content)
        {

            MessageDTO dto = new MessageDTO
            {
                Content = content
            };

            string json = JsonConvert.SerializeObject(dto);

            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", serv.Token));
            HttpResponseMessage res = await client.PostAsync(new Uri($"https://flightappapi.azurewebsites.net/api/Flight/messages/{friend.Id}"), 
                new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            return res.IsSuccessStatusCode;
        }

        public static async Task<IList<Message>> GetMessagesAsync(Passenger friend)
        {
            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {serv.Token}");
            string json = await client.GetStringAsync(new Uri($"https://flightappapi.azurewebsites.net/api/Flight/messages/{friend.Id}"));
            IList<Message> messages = JsonConvert.DeserializeObject<IList<Message>>(json);
            return messages;
        }

        public static async Task<bool> PostSwap(Seat from, Seat to)
        {
            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", serv.Token));
            HttpResponseMessage res = await client.PostAsync(new Uri($"https://flightappapi.azurewebsites.net/api/Flight/move_passenger/{from.Id}/{to.Id}"), null);
            return res.IsSuccessStatusCode;
        }

        public class MessageDTO
        {
            public string Content { get; set; }
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
