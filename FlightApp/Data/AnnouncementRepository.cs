using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using FlightApp.Model;
using Newtonsoft.Json;

namespace FlightApp.Data
{
    public static class AnnouncementRepository
    {
        public static async Task<IList<Announcement>> GetAllAsync()
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(new Uri("http://localhost:49681/api/Announcement/get_all"));
            IList<Announcement> announcements = JsonConvert.DeserializeObject<ObservableCollection<Announcement>>(json);
            return announcements;
        }
    }
}
