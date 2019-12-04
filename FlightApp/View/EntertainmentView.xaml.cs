using FlightApp.Model;
using FlightApp.ViewModel;
using System;
using Windows.Media.Core;
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

        private void Music_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Music music = (sender as ListView).SelectedItem as Music;
            MusicPlayer.Source = MediaSource.CreateFromUri(
                new Uri($"ms-appx:///Assets/Files/{music.Source}"));
        }
    }
}
