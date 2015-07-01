using System.ComponentModel.DataAnnotations;

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
    }
}