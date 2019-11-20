using FlightApp.Utils;

namespace FlightApp.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private UserService _userService;

        public MainPageViewModel() {
            _userService = UserService.GetInstance();
        }

        public void Logout()
        {
            _userService.Reset();
        }
    }
}
