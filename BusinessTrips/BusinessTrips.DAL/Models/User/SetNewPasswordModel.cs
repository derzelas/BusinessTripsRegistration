using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Repositories;

namespace BusinessTrips.DAL.Models.User
{
    public class SetNewPasswordModel
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

        public void SetPassword()
        {
            var userRepository = new UserRepository();
            var userEntity = userRepository.GetBy(Id);

            var password = new Password(ConfirmedPassword);
            userEntity.HashedPassword = password.GetHashed();
            userEntity.Salt = password.GetSalt();

            userRepository.SaveChanges();
        }
    }
}