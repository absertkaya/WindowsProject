using System;
using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public DateTime TimeStamp { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Content { get; set; }
        public Staff Sender { get; set; }
    }
}
