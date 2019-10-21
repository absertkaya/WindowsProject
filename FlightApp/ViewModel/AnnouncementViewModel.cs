using FlightApp.Data;
using FlightApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Announcements = new DummyData().Announcements;
        }
    }
}
