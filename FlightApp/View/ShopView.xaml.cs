using FlightApp.Model;
using FlightApp.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FlightApp.View
{
    public sealed partial class ShopView : Page
    {
        public ShopViewModel viewModel { get; set; }

        public ShopView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            viewModel = new ShopViewModel();
            DataContext = viewModel;
        }

        private void ShowCart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            split.IsPaneOpen = !split.IsPaneOpen;
        }

        private void AddToCart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            split.IsPaneOpen = true;
            viewModel.AddToCart((sender as Button).DataContext as Product);
        }

        private void IncrementCart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            viewModel.AddToCart(((sender as AppBarButton).DataContext as OrderLine).Product);
        }

        private void DecrementCart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            viewModel.DecrementFromCart(((sender as AppBarButton).DataContext as OrderLine).Product);
        }

        private void RemoveCart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            viewModel.RemoveFromCart(((sender as AppBarButton).DataContext as OrderLine).Product);
        }

        private void PlaceOrder(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            viewModel.PlaceOrder();
        }

        private void ShowHistory(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainPage.Instance.NavigateToOrderHistory();
        }
    }
}
