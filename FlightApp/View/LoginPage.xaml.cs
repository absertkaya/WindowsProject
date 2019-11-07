using FlightApp.ViewModel;
using Windows.UI.Xaml.Controls;

namespace FlightApp
{
    public sealed partial class LoginPage : Page
    {
        private LoginPageViewModel _viewModel;
        public LoginPage()
        {
            this.InitializeComponent();
            _viewModel = new LoginPageViewModel();
            DataContext = _viewModel;
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _viewModel.Login();
        }
    }
}
