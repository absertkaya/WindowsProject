using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FlightApp.View
{
    public sealed partial class ProductPage : Page
    {
        public ProductPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = e.Parameter;
        }
    }
}
