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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            MusicPlayer.Source = null;
        }

        private void Music_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Music music = (sender as ListView).SelectedItem as Music;
            MusicPlayer.Source = MediaSource.CreateFromUri(
                new Uri($"ms-appx:///Assets/Files/{music.Source}"));
        }

        private void Movie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainPage.Instance.NavigateToMoviePlayer((sender as ListView).SelectedItem as Movie);
        }
    }
}
