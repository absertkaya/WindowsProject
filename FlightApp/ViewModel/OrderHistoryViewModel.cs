using FlightApp.Model;
using FlightApp.Utils;
using System.Linq;

namespace FlightApp.ViewModel
{
    public class OrderHistoryViewModel : ViewModelBase
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

        public OrderHistoryViewModel()
        {
            Orders = (UserService.GetInstance().User as Passenger).Orders.OrderByDescending(o => o.TimeStamp);
            SelectedOrder = Orders.First();
        }
        public void SelectOrder(Order order)
        {
            SelectedOrder = order;
        }
    }
}
