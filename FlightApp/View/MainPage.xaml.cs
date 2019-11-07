using FlightApp.Model;
using FlightApp.Utils;
using FlightApp.View;
using FlightApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace FlightApp
{
    public sealed partial class MainPage : Page
    {
        private MainPageViewModel _viewModel;
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type page)> {
            ("home", typeof(HomePage)),

            ("announcements", typeof(AnnouncementView)),
            ("shop", typeof(Page)),
            ("messenger", typeof(Page)),
            ("music", typeof(Page)),
            ("video", typeof(Page)),
            ("games", typeof(Page)),

            ("manageAnnouncement", typeof(Page)),
            ("handleOrder", typeof(Page)),
            ("passengers", typeof(Page)),
            ("flightDetails", typeof(Page))
        };

        public MainPage()
        {
            this.InitializeComponent();
            _viewModel = new MainPageViewModel();
            DataContext = _viewModel;

            UserService inst = UserService.GetInstance();
            inst.PropertyChanged += (sender, args) => {
                if (args.PropertyName.Equals("User"))
                {
                    if(inst.User is null)
                    {
                        ContentFrame.Navigate(typeof(LoginPage), null, new EntranceNavigationTransitionInfo());
                        NavView.Header = "Login";
                    }
                    else
                    {
                        NavView_Navigate("home", new EntranceNavigationTransitionInfo());
                        switch (inst.User.Type)
                        {
                            case UserType.PASSENGER:
                                GeneratePassengerMenu();
                                break;
                            case UserType.STAFF:
                                GenerateStaffMenu();
                                break;
                        }
                    }
                }
            };
        }

        #region Navigation
        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            NavView.SelectedItem = NavView.MenuItems[0];
            NavView_Navigate("home", new EntranceNavigationTransitionInfo());
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                string navItemTag = args.SelectedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type _page = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag)).Page;

            if (!(_page is null)) ContentFrame.Navigate(_page, null, transitionInfo);
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);
                if (item.Tag != null)
                {
                    NavView.SelectedItem = NavView.MenuItems
                        .OfType<NavigationViewItem>()
                        .First(n => n.Tag.Equals(item.Tag));
                    NavView.Header =
                    ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
                }
            }
        }

        private void Login(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(LoginPage), null, new EntranceNavigationTransitionInfo());
            NavView.Header = "Login";
        }

        private void Logout(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }
        #endregion

        #region Menu
        private void GeneratePassengerMenu()
        {
            IEnumerable<NavigationViewItem> items = NavView.MenuItems.OfType<NavigationViewItem>().Where(i =>
                     i.Tag.Equals("announcements") ||
                     i.Tag.Equals("shop") ||
                     i.Tag.Equals("messenger") ||
                     i.Tag.Equals("music") ||
                     i.Tag.Equals("video") ||
                     i.Tag.Equals("games") ||
                     i.Tag.Equals("flightDetails")
            );

            foreach (NavigationViewItem item in items) {
                item.Visibility = Visibility.Visible;
            }

            NavViewFooter.Children.OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals("login"))
                    .Visibility = Visibility.Collapsed;

            NavViewFooter.Children.OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals("logout"))
                    .Visibility = Visibility.Visible;
        }

        private void GenerateStaffMenu()
        {
            IEnumerable<NavigationViewItem> items = NavView.MenuItems.OfType<NavigationViewItem>().Where(i =>
                     i.Tag.Equals("manageAnnouncement") ||
                     i.Tag.Equals("handleOrder") ||
                     i.Tag.Equals("passengers") ||
                     i.Tag.Equals("flightDetails")
            );

            foreach (NavigationViewItem item in items)
            {
                item.Visibility = Visibility.Visible;
            }

            NavViewFooter.Children.OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals("login"))
                    .Visibility = Visibility.Collapsed;

            NavViewFooter.Children.OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals("logout"))
                    .Visibility = Visibility.Visible;
        }
        #endregion

    }
}
