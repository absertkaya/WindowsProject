using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FlightApp.Model;
using FlightApp.Utils;
using Newtonsoft.Json;
using Windows.Web.Http;

namespace FlightApp.Data
{
    public static class AnnouncementRepository
    {
        public static async Task<IList<Announcement>> GetAllAsync()
        {
            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", serv.Token));
            string json = await client.GetStringAsync(new Uri($"http://localhost:49681/api/Announcement/get_by_flight/{serv.User.FlightId}"));
            IList<Announcement> announcements = JsonConvert.DeserializeObject<ObservableCollection<Announcement>>(json);
            return announcements;
        }



        public static async Task<bool> PostAnnouncement(string title, string content, Passenger passenger)
        {
            AnnouncementDTO dto = new AnnouncementDTO
            {
                Title = title,
                Content = content,
                PassengerId = passenger.Id
            };

            string json = JsonConvert.SerializeObject(dto);

            UserService serv = UserService.GetInstance();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", serv.Token));
            HttpResponseMessage res = await client.PostAsync(new Uri($"http://localhost:49681/api/Announcement/create_by_flight/{serv.User.FlightId}"),
                new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }

    public class AnnouncementDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public int PassengerId { get; set; }
    }
}
