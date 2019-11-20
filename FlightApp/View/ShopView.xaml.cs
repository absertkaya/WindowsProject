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

        private void Show_Cart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            split.IsPaneOpen = !split.IsPaneOpen;
        }

        private void Click_Product(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            MainPage.Instance.NavigateToProduct((sender as StackPanel).DataContext as Product);
        }
    }
}
