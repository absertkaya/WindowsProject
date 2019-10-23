using FlightApp.Data;
using FlightApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.ViewModel
{
    public class AnnouncementViewModel
    {
        public IList<Announcement> Announcements { get; set; }

        public AnnouncementViewModel()
        {
        }

        public async Task CallService()
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("http://localhost:49681/api/Announcement/get_all"));
            var lst = JsonConvert.DeserializeObject<ObservableCollection<Announcement>>(json);
            Announcements = lst;
        }
    }
}
