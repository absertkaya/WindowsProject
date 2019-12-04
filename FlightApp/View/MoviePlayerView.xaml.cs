using FlightApp.Model;
using System;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FlightApp.View
{
    public sealed partial class MoviePlayerView : Page
    {

        private Movie _movie;

        public MoviePlayerView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _movie = e.Parameter as Movie;
            DataContext = _movie;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            MoviePlayer.Source = null;
        }

        private void MoviePlayer_Loaded(object sender, RoutedEventArgs e)
        {
            MoviePlayer.Source = MediaSource.CreateFromUri(
                new Uri($"ms-appx:///Assets/Files/{_movie.Source}"));
        }
    }
}
