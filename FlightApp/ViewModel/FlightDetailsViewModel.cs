﻿using FlightApp.Data;
using FlightApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FlightApp.ViewModel
{
    public class FlightDetailsViewModel : ViewModelBase
    {
        private Weather _weather;

        public Weather Weather {
            get { return _weather; }
            set { _weather = value; RaisePropertyChanged(); }
        }

        private Flight _flight;

        public Flight Flight {
            get { return _flight; }
            set { _flight = value; RaisePropertyChanged(); }
        }

        public FlightDetailsViewModel()
        {
            LoadFlight();
        }

        private async void LoadWeather(string city)
        {
            try
            {
                Weather = await FlightRepository.GetWeatherAsync(city);
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

        private async void LoadFlight()
        {
            try
            {
                Flight = await FlightRepository.GetFlightDetailsAsync();
                string city = Flight.ArrivalDest.Split(" ")[0];
                LoadWeather(city);
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
                    LoadFlight();
                    break;
            }
        }
    }
}
