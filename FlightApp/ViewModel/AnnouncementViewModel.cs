using FlightApp.Data;
using FlightApp.Model;
using System;
using System.Collections.Generic;
using Windows.UI.Popups;

namespace FlightApp.ViewModel
{
    public class AnnouncementViewModel : ViewModelBase
    {
        private IList<Announcement> _announcements;
        public IList<Announcement> Announcements {
            get { return _announcements; }
            set { _announcements = value; RaisePropertyChanged(); }
        }

        public AnnouncementViewModel() {
            LoadAnnouncements();
        }

        private async void LoadAnnouncements()
        {
            try
            {
                Announcements = await AnnouncementRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                MessageDialog messageDialog = new MessageDialog($"Couldn't establish a connection to the database. \n{e.Message}");
                messageDialog.Commands.Add(new UICommand("Try again", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                await messageDialog.ShowAsync();
            }
        }

        private async void LoadPersonalAnnouncements()
        {
            try
            {
                Announcements = await AnnouncementRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                MessageDialog messageDialog = new MessageDialog($"Couldn't establish a connection to the database. \n{e.Message}");
                messageDialog.Commands.Add(new UICommand("Try again", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                await messageDialog.ShowAsync();
            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            switch (command.Label)
            {
                case "Try again":
                    LoadAnnouncements();
                    break;
            }
        }
    }
}
