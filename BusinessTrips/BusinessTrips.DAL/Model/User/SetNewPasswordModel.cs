using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Repository;
namespace BusinessTrips.DAL.Model.User
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

            ConfirmedPassword = PasswordHasher.GetHashed(ConfirmedPassword + userEntity.Salt);
            userEntity.HashedPassword = ConfirmedPassword;

            userRepository.SaveChanges();
        }
    }
}