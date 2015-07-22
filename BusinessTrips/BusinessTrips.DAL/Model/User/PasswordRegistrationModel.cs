using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.DAL.Model.User
{
    public class PasswordRegistrationModel
    {
        private const int MinimumPasswordLength = 6;
        private const string PasswordValidationMessage = "Minimum password length is 6";

        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(MinimumPasswordLength, ErrorMessage = PasswordValidationMessage)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm password")]
        public string ConfirmedPassword { get; set; }
    }
}