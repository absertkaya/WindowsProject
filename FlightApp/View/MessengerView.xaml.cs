using FlightApp.Model;
using FlightApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlightApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MessengerView : Page
    {

        public MessengerViewModel _vm { get; set; }

        public MessengerView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _vm = new MessengerViewModel();
            DataContext = _vm;
        }

        private void StackPanel_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (sv.Visibility == Visibility.Collapsed)
            {
                sv.Visibility = Visibility.Visible;
            }
            Passenger friend = (sender as StackPanel).DataContext as Passenger;
            _vm.SelectedFriend = friend;
            _vm.LoadMessages(friend);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.PostMessage(msgBox.Text);
        }
    }
}
