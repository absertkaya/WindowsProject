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
    }
}
