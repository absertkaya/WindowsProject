using System;
using FlightApp.Data;
using Windows.UI.Popups;

namespace FlightApp.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public async void Login()
        {
            try
            {
                if (!await UserRepository.LoginAsync(Email, Password))
                {
                    MessageDialog messageDialog = new MessageDialog("Invalid credentials provided. \nPlease try again.");
                    messageDialog.Commands.Add(new UICommand("Close"));
                    messageDialog.CancelCommandIndex = 0;
                    await messageDialog.ShowAsync();
                }
            }
            catch(Exception e)
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
                    Login();
                    break;
            }
        }
    }
}
