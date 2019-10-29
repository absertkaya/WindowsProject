using FlightApp.Model;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace FlightApp.Data
{
    public static class UserRepository
    {
        public static string Token { get; private set; }
        public static ApplicationUser User { get; private set; }

        public static async Task<bool> RegisterAsync(Passenger user, string password, string passwordConfirmation)
        {
            RegisterDTO registerDTO = new RegisterDTO() {
                Email = user.Email,
                Password = password,

                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordConfirmation = passwordConfirmation,
                BirthDate = user.BirthDate
            };
            string content = JsonConvert.SerializeObject(registerDTO);

            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.PostAsync(new Uri("http://localhost:49681/api/Account"),
                new HttpStringContent(content, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

            if (res.IsSuccessStatusCode)
            {
                Token = JsonConvert.DeserializeObject<string>(res.Content.ToString());
                return true;
            }
            return false;
        }

        public static async Task<bool> LoginAsync(string email, string password)
        {
            LoginDTO loginDTO = new LoginDTO()
            {
                Email = email,
                Password = password
            };
            string content = JsonConvert.SerializeObject(loginDTO);

            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.PostAsync(new Uri("http://localhost:49681/api/Account/login"),
                new HttpStringContent(content, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

            if (res.IsSuccessStatusCode)
            {
                Token = JsonConvert.DeserializeObject<string>(res.Content.ToString());
                return true;
            }
            return false;
        }

        #region DTOs
        private class LoginDTO
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        private class RegisterDTO : LoginDTO
        {
            [Required]
            [StringLength(255)]
            public string FirstName { get; set; }

            [Required]
            [StringLength(255)]
            public string LastName { get; set; }

            [Required]
            [Compare("Password")]
            [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
            public string PasswordConfirmation { get; set; }

            [Required]
            public DateTime BirthDate { get; set; }
        }
        #endregion
    }
}
