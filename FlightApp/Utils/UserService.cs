using FlightApp.Model;
using GalaSoft.MvvmLight;

namespace FlightApp.Utils
{
    public class UserService : ObservableObject
    {
        private static UserService INSTANCE = new UserService();
        private ApplicationUser _user;

        public ApplicationUser User {
            get { return _user; }
            set { Set(ref _user, value); }
        }
        public string Token { get; set; }

        public static UserService GetInstance()
        {
            return INSTANCE;
        }

        public void Reset()
        {
            User = null;
            Token = null;
        }
    }
}
