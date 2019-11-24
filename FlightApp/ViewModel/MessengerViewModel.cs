using FlightApp.Data;
using FlightApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FlightApp.ViewModel
{
    public class MessengerViewModel : ViewModelBase
    {
        private IList<Passenger> _friends;

        public IList<Passenger> Friends {
            get { return _friends; }
            set { _friends = value; RaisePropertyChanged(); }
        }

        private IList<Message> _messages;

        public IList<Message> Messages {
            get { return _messages; }
            set { _messages = value; RaisePropertyChanged(); }
        }


        private Passenger _selectedFriend;

        public Passenger SelectedFriend {
            get { return _selectedFriend; }
            set { _selectedFriend = value; RaisePropertyChanged(); }
        }


        public MessengerViewModel()
        {
            LoadFriends();
        }

        public async void PostMessage(string content)
        {
            try
            {
                if (!await FlightRepository.PostMessage(SelectedFriend, content))
                {
                    MessageDialog messageDialog = new MessageDialog("Invalid message. \nPlease try again.");
                    messageDialog.Commands.Add(new UICommand("Close"));
                    messageDialog.CancelCommandIndex = 0;
                    await messageDialog.ShowAsync();
                }
                LoadMessages(SelectedFriend);
            }
            catch (Exception e)
            {
                MessageDialog messageDialog = new MessageDialog($"Couldn't establish a connection to the database. \n{e.Message}");
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                await messageDialog.ShowAsync();
            }
        }

        public async void LoadMessages(Passenger friend)
        {
            try
            {
                Messages = new List<Message>(await FlightRepository.GetMessagesAsync(friend));
            }
            catch (Exception e)
            {
                MessageDialog messageDialog = new MessageDialog($"Couldn't establish a connection to the database. \n{e.Message}");
                messageDialog.Commands.Add(new UICommand("Try again", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                await messageDialog.ShowAsync();
            }
        }

        private async void LoadFriends()
        {
            try
            {
                Friends = new List<Passenger>(await FlightRepository.GetFriendsAsync());
            }
            catch (Exception e)
            {
                MessageDialog messageDialog = new MessageDialog($"Couldn't establish a connection to the database. \n{e.Message}");
                messageDialog.Commands.Add(new UICommand("Try again", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                await messageDialog.ShowAsync();
            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            switch (command.Label)
            {
                case "Try again":
                    LoadFriends();
                    break;
            }
        }
    }
}
