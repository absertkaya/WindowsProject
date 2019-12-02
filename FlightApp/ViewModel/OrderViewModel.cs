using FlightApp.Data;
using FlightApp.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FlightApp.ViewModel
{
    public class OrderViewModel : ViewModelBase
    {
        #region Properties
        private IOrderedEnumerable<Order> _orders;
        private Order _selectedOrder;
        public IOrderedEnumerable<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; RaisePropertyChanged(); }
        }
        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set { _selectedOrder = value; RaisePropertyChanged(); }
        }
        #endregion

        public OrderViewModel()
        {
            LoadOrders();
        }

        public void SelectOrder(Order order)
        {
            SelectedOrder = order;
        }
        
        public async Task HandleOrderAsync()
        {
            try
            {
                await ShopRepository.AcceptOrderAsync(SelectedOrder);
                int id = SelectedOrder.Id;
                LoadOrders();
                SelectedOrder = Orders.First(o => o.Id.Equals(id));
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

        private async void LoadOrders()
        {
            try
            {
                Orders = (await ShopRepository.GetOrdersAsync()).OrderByDescending(o => o.TimeStamp);
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
                    LoadOrders();
                    break;
            }
        }
    }
}
