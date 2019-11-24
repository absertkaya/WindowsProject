using FlightApp.Data.DTOs;
using FlightApp.Model;
using FlightApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace FlightApp.Data
{
    public class UserRepository
    {
        public static async Task<bool> LoginAsync(string email, string password)
        {
            LoginDTO loginDTO = new LoginDTO()
            {
                Email = email,
                Password = password
            };
            string content = JsonConvert.SerializeObject(loginDTO);

            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.PostAsync(new Uri("http://localhost:49681/api/Account"),
                new HttpStringContent(content, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

            if (res.IsSuccessStatusCode)
            {
                LoginResponseDTO response = JsonConvert.DeserializeObject<LoginResponseDTO>(res.Content.ToString());
                UserService serv = UserService.GetInstance();
                serv.Token = response.Token;
                serv.User = response.ToUser();
                return true;
            }
            return false;
        }

        public static async Task<IList<Passenger>> GetAllPassengersAsync()
        {
            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {serv.Token}");
            string json = await client.GetStringAsync(new Uri($"http://localhost:49681/api/Flight/{serv.User.FlightId}/get_passengers"));
            IList<Passenger> passengers = JsonConvert.DeserializeObject<IList<Passenger>>(json);
            return passengers;
        }

        #region DTOs
        private class LoginDTO
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }
        private class LoginResponseDTO
        {
            public string Token { get; set; }
            public int Id { get; set; }
            public DateTime BirthDate { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public List<Friend> Friends { get; set; }
            public int SeatNr { get; set; }
            public ClassType SeatClass { get; set; }
            public int FlightId { get; set; }
            public UserType Type { get; set; }
            public IList<OrderDTO> Orders { get; set; }

            public ApplicationUser ToUser()
            {
                switch (Type)
                {
                    case UserType.PASSENGER:
                        return new Passenger
                        {
                            Id = this.Id,
                            BirthDate = this.BirthDate,
                            Email = this.Email,
                            FirstName = this.FirstName,
                            LastName = this.LastName,
                            Friends = this.Friends,
                            Seat = new Seat { Nr = SeatNr, ClassType = SeatClass },
                            Type = this.Type,
                            FlightId = this.FlightId,
                            Orders = this.Orders.Select(o => o.ToOrder()).ToList()
                        };
                    case UserType.STAFF:
                        return new Staff
                        {
                            Id = this.Id,
                            BirthDate = this.BirthDate,
                            Email = this.Email,
                            FirstName = this.FirstName,
                            LastName = this.LastName,
                            Type = this.Type,
                            FlightId = this.FlightId
                        };
                }
                return null;
            }
        }
        #endregion
    }
}
