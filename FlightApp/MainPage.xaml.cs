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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace FlightApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type page)> {
            ("home", typeof(HomePage)),
            ("login", typeof(LoginPage)),
            ("register", typeof(RegisterPage)),
            ("createAnnouncement", typeof(CreateAnnouncementPage)),
            ("handleOrder", typeof(HandleOrderPage)),
            ("placeOrder", typeof(MakeOrderPage)),
            ("viewAnnouncements", typeof(ViewAnnouncementsPage)),
            ("createMessage", typeof(CreateMessagePage)),
            ("viewMessages", typeof(ViewMessagesPage)),
            ("orderHistory", typeof(OrderHistoryPage)),
            ("games", typeof(GamesPage)),
            ("music", typeof(MusicPage)),
            ("video", typeof(VideoPage)),
            ("passengers", typeof(PassengersPage))
        };

        public MainPage()
        {
            this.InitializeComponent();
        }

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
            var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
            Type _page = item.Page;
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (!(_page is null) && !Equals(preNavPageType, _page))
            {
                ContentFrame.Navigate(_page, null, transitionInfo);
            }
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;
            if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);
                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));
                NavView.Header =
                ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }

        private void NavView_BackRequested(NavigationView sender,
                                   NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }

        private void BackInvoked(KeyboardAccelerator sender,
                         KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private bool On_BackRequested()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }

    }
}
