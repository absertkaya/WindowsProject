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
            //call api
            Announcements = new ObservableCollection<Announcement>();
        }

        public async void CallService()
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("https://localhost:44300/api/announcements/get_all"));
            var lst = JsonConvert.DeserializeObject<IList<Announcement>>(json);
            foreach (var announcement in lst)
            {
                Announcements.Add(announcement);
            }
        }
    }
}
