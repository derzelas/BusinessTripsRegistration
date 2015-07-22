using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.DAL.Model.User
{
    public class PasswordRegistrationModel
    {
        private const int MinimumPasswordLength = 6;

        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(MinimumPasswordLength, ErrorMessage = "Minimum password length is 6")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match confirmation password.")]
        [Display(Name = "Confirm password")]
        public string ConfirmedPassword { get; set; }
    }
}