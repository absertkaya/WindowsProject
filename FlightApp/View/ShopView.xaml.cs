using FlightApp.Model;
using FlightApp.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FlightApp.View
{
    public sealed partial class ShopView : Page
    {
        public ShopViewModel ViewModel { get; set; }

        public ShopView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel = new ShopViewModel();
            DataContext = ViewModel;
        }

        private void ShowCart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            split.IsPaneOpen = !split.IsPaneOpen;
        }

        private void AddToCart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.AddToCart((sender as Button).DataContext as Product);
        }

        private void IncrementCart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.AddToCart(((sender as AppBarButton).DataContext as OrderLine).Product);
        }

        private void DecrementCart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.DecrementFromCart(((sender as AppBarButton).DataContext as OrderLine).Product);
        }

        private void RemoveCart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.RemoveFromCart(((sender as AppBarButton).DataContext as OrderLine).Product);
        }

        private void PlaceOrder(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.PlaceOrder();
        }
    }
}
