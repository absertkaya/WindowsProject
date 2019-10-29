using GalaSoft.MvvmLight;
using System;
using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class Announcement : ObservableObject
    {
        private DateTime _timestamp;
        private string _title;
        private string _content;

        public int AnnouncementId { get; set; }

        [Required]
        public DateTime TimeStamp {
            get { return _timestamp; }
            set { Set("TimeStamp", ref _timestamp, value); }
        }

        [Required]
        [MaxLength(100)]
        public string Title {
            get { return _title; }
            set { Set("Title", ref _title, value); }
        }

        [Required]
        [MaxLength(255)]
        public string Content {
            get { return _content; }
            set { Set("Content", ref _content, value); }
        }

        public Announcement()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
