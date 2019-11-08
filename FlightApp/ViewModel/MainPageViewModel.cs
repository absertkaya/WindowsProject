using FlightApp.Utils;

namespace FlightApp.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private int _accessLevel;
        private UserService _userService;

        public int AccessLevel
        {
            get { return _accessLevel; }
            private set { _accessLevel = value; RaisePropertyChanged(); }
        }

        public MainPageViewModel() {
            _accessLevel = 0;
            _userService = UserService.GetInstance();
        }
    }
}
