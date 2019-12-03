using FlightApp.Model;
using FlightApp.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FlightApp.View
{
    public sealed partial class OrderPage : Page
    {
        public OrderViewModel viewModel { get; set; }

        public OrderPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            viewModel = new OrderViewModel();
            DataContext = viewModel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order order = list.SelectedItem as Order;
            viewModel.SelectOrder(order);
            if (order is null || viewModel.SelectedOrder.OrderStatus.Equals(OrderStatus.HANDLED))
                btnAccept.Visibility = Visibility.Collapsed;
            else btnAccept.Visibility = Visibility.Visible;
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            await viewModel.HandleOrderAsync();
        }
    }
}
