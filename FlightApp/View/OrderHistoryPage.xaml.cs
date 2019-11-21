using FlightApp.Model;
using FlightApp.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FlightApp.View
{
    public sealed partial class OrderHistoryPage : Page
    {
        public OrderHistoryViewModel viewModel { get; set; }

        public OrderHistoryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            viewModel = new OrderHistoryViewModel();
            DataContext = viewModel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SelectOrder(list.SelectedItem as Order);
        }
    }
}
