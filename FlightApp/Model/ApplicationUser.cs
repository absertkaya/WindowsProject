using System;
using System.ComponentModel.DataAnnotations;

namespace FlightApp.Model
{
    public class ApplicationUser
    {
        public int ApplicationUserId { get; set; }
        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
