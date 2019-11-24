using FlightApp.Data;
using FlightApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FlightApp.ViewModel
{
    public class ManagePassengersViewModel : ViewModelBase
    {
        private IList<Seat> _seats;

        public IList<Seat> Seats {
            get { return _seats; }
            set { _seats = value; RaisePropertyChanged(); }
        }

        private IEnumerable<Seat> _seatsWithPassengers;

        public IEnumerable<Seat> SeatsWithPassengers {
            get { return _seatsWithPassengers; }
            set { _seatsWithPassengers = value; RaisePropertyChanged(); }
        }


        public ManagePassengersViewModel()
        {
            LoadSeats();
            
        }

        public async void PostSwap(Seat from, Seat to)
        {
            try
            {
                if (!await FlightRepository.PostSwap(from, to))
                {
                    MessageDialog messageDialog = new MessageDialog("Invalid swap. \nPlease try again.");
                    messageDialog.Commands.Add(new UICommand("Close"));
                    messageDialog.CancelCommandIndex = 0;
                    await messageDialog.ShowAsync();
                }
                LoadSeats();
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

        private async void LoadSeats()
        {
            try
            {
                Seats = new List<Seat>(await FlightRepository.GetAllSeatsAsync());
                SeatsWithPassengers = Seats.Where(s => s.Passenger != null);
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
                    LoadSeats();
                    break;
            }
        }
    }
}
