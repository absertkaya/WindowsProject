using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Model
{
    public class Announcement : ObservableObject
    {
        private int _id;

        public int AnnouncementId {
            get;
        }

        private DateTime _timestamp;

        public DateTime TimeStamp {
            get { return _timestamp; }
            set { Set("TimeStamp", ref _timestamp, value); }
        }

        private string _title;

        public string Title {
            get { return _title; }
            set { Set("Title", ref _title, value); }
        }

        private string _content;

        public string Content {
            get { return _content; }
            set { Set("Content", ref _content, value); }
        }

        public Announcement(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public Announcement()
        {

        }
    }
}
