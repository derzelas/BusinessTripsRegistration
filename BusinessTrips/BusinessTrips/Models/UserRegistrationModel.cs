using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using BusinessTrips.DataAccessLayer;

//e de la e mail

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

        public void Save(UserRegistrationModel userFields)
        {
            IStorage<UserRegistrationModel> storage = new InMemoryStorage<UserRegistrationModel>();

            var registrationRepository = new UserRegistrationRepository(storage);
            registrationRepository.Add(userFields);
            Email a = new Email();
            a.Send();
        }
    }

    public class Email
    {
        public void Send()
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            var message = new MailMessage();
            message.From = new MailAddress("thezohan123456789@gmail.com");
            message.To.Add("petrica.bota2@yahoo.com");
            message.Body = "Hello World";
            message.Subject = "hope this works";
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("thezohan123456789@gmail.com", "FieruBateOsu123!@#");
            client.Send(message);
            message = null;
        }
    }


}