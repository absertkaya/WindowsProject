using System.Collections.Generic;

namespace FlightApp.Model
{
    public class Staff : ApplicationUser
    {
        public IList<Announcement> SentAnnouncements { get; set; }
        public Flight Flight { get; set; }
    }
}
