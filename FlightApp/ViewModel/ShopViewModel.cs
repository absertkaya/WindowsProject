using FlightApp.Data;
using FlightApp.Model;
using System;
using System.Collections.Generic;
using Windows.UI.Popups;

namespace FlightApp.ViewModel
{
    public class ShopViewModel : ViewModelBase
    {
        private IList<Product> _products;
        public IList<Product> Products
        {
            get { return _products; }
            set { _products = value; RaisePropertyChanged(); }
        }

        public ShopViewModel()
        {
            LoadProducts();
        }

        private async void LoadProducts()
        {
            try
            {
                Products = await ShopRepository.GetAllProductsAsync();
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
                    LoadProducts();
                    break;
            }
        }
    }
}
