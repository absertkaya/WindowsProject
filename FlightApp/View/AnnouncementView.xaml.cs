using FlightApp.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FlightApp.View
{
    public sealed partial class AnnouncementView : Page
    {
        public AnnouncementViewModel viewModel { get; set; }

        public AnnouncementView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            viewModel = new AnnouncementViewModel();
            DataContext = viewModel;
        }
    }
}
