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
            if(! await UserRepository.LoginAsync(Email, Password))
            {
                MessageDialog messageDialog = new MessageDialog("Invalid credentials provided. \nPlease try again.");
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.CancelCommandIndex = 0;
                await messageDialog.ShowAsync();
            }
        }
    }
}
