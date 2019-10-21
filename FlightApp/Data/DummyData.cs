using FlightApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Data
{
    public class DummyData
    {
        public IList<Announcement> Announcements { get; set; }

        public DummyData()
        {
            Announcements = new List<Announcement>() {
                new Announcement("A1", "C1"),
                new Announcement("A2", "C2"),
                new Announcement("A3", "C3"),
                new Announcement("A4", "C4"),
                new Announcement("A5", "C5"),
                new Announcement("A6", "C6"),
                new Announcement("A7", "C7"),
                new Announcement("A8", "C8"),
                new Announcement("A9", "C9"),
            };
        }
    }
}
