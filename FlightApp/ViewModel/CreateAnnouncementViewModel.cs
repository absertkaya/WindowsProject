using FlightApp.Data;
using FlightApp.Model;
using FlightApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FlightApp.ViewModel
{
    public class CreateAnnouncementViewModel : AnnouncementViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        private IList<Passenger> _passengers;

        public IList<Passenger> Passengers {
            get { return _passengers; }
            set { _passengers = value; RaisePropertyChanged(); }
        }

        public CreateAnnouncementViewModel()
        {
            LoadPassengers();
        }

        private async void LoadPassengers()
        {
            try
            {
                Passengers = new ObservableCollection<Passenger>(await UserRepository.GetAllPassengersAsync());
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

        public async void PostAnnouncement(Passenger passenger)
        {
            try
            {
                if (!await AnnouncementRepository.PostAnnouncement(Title, Content, passenger))
                {
                    MessageDialog messageDialog = new MessageDialog("Invalid announcement. \nPlease try again.");
                    messageDialog.Commands.Add(new UICommand("Close"));
                    messageDialog.CancelCommandIndex = 0;
                    await messageDialog.ShowAsync();
                }
                LoadAnnouncements();
            }
            catch (Exception e)
            {
                MessageDialog messageDialog = new MessageDialog($"Couldn't establish a connection to the database. \n{e.Message}");
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
                    LoadPassengers();
                    break;
            }
        }
    }
}
