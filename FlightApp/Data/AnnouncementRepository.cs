using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using FlightApp.Model;
using FlightApp.Utils;
using Newtonsoft.Json;

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
    }
}
