using FlightApp.Data;
using FlightApp.Model;
using System.Collections.Generic;

namespace FlightApp.ViewModel
{
    public class AnnouncementViewModel : ViewModelBase
    {
        private IList<Announcement> _announcements;
        public IList<Announcement> Announcements {
            get { return this._announcements; }
            set { this._announcements = value; RaisePropertyChanged(); }
        }

        public AnnouncementViewModel() {
            LoadAnnouncements();
        }

        private async void LoadAnnouncements()
        {
            this.Announcements = await AnnouncementRepository.GetAllAsync();
        }
    }
}
