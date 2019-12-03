using FlightApp.Model;
using FlightApp.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FlightApp.View
{
    public sealed partial class EntertainmentView : Page
    {
        public EntertainmentViewModel viewModel { get; set; }

        public EntertainmentView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            viewModel = new EntertainmentViewModel();
            DataContext = viewModel;
        }
    }
}
