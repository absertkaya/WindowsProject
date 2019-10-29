using System;

namespace FlightApp.Model
{
    public class ApplicationUser
    {
        public int ApplicationUserId { get; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
    }
}
