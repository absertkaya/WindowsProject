using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class StaffFlight
    {
        [Required]
        public Staff Staff { get; set; }
        [Required]
        public Flight Flight { get; set; }

        public IList<Announcement> Announcements { get; set; }

        protected StaffFlight()
        {
            Announcements = new List<Announcement>();
        }

        public void AddAnnouncement(Announcement announcement)
        {
            Announcements.Add(announcement);
        }
    }
}
