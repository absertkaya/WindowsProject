using System;
using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string LastName { get; set; }
        [MaxLength(40)]
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public UserType Type { get; set; }
    }
}
