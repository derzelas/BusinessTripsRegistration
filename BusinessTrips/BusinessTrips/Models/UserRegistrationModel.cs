using System.ComponentModel.DataAnnotations;
using System.Net; 
using System.Net.Mail;//e de la e mail
using BusinessTrips.DataAccessLayer;

namespace BusinessTrips.Models
{
    public class UserRegistrationModel
    {
        private const int MinimumPasswordLength = 6;
        private const int MinimumNameLength = 3;
        private const string PasswordValidationMessage = "Minimum password length is 6";

        [Required]
        [Display(Name = "Name")]
        [MinLength(MinimumNameLength, ErrorMessage = "Name length must be at least 3 characters long")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(MinimumPasswordLength, ErrorMessage = PasswordValidationMessage)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(MinimumPasswordLength, ErrorMessage = PasswordValidationMessage)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm password")]
        public string ConfirmedPassword { get; set; }

        public void Save()
        {
            IStorage<UserRegistrationModel> storage = InMemoryStorage<UserRegistrationModel>.GetInstace();
            var registrationRepository = new UserRegistrationRepository(storage);
            registrationRepository.Add(this);
            BusinessTrips.Models.Email.Send(this);
        }
    }

    public static class Email
    { 
        public static void Send(UserRegistrationModel user)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            var message = new MailMessage();
            message.From = new MailAddress("iQuestBusinessTrips@gmail.com");
            message.To.Add(user.Email);
            message.Body = "Hello World 2.0";
            message.Subject = "hope this works";
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("iQuestBusinessTrips@gmail.com", "Ana@re6mere");
            client.Send(message);
            message = null;
        }
    }


}