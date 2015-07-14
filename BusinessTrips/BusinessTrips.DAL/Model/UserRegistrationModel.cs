using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
{
    public class UserRegistrationModel
    {
        private const int MinimumPasswordLength = 6;
        private const int MinimumNameLength = 3;
        private const string PasswordValidationMessage = "Minimum password length is 6";

        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MinLength(MinimumNameLength, ErrorMessage = "Name length must be at least 3 characters long")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        [UniqueEmail(ErrorMessage = "This e-mail is already registered")]
        public string Email { get; set; }

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

        public void Save()
        {
            Id = Guid.NewGuid();

            UserEntity userEntity = ToUserEntity();
            userEntity.Roles.Add(new Role(){Id = 1, Name = "Admin"});
            userEntity.Salt = Guid.NewGuid().ToString();
            userEntity.HashedPassword = PasswordHasher.HashPassword(userEntity.HashedPassword + userEntity.Salt);

            var userRepository = new UserRepository();
            userRepository.CreateByUserEntity(userEntity);
            userRepository.CommitChanges();
        }

        public UserEntity ToUserEntity()
        {
            return new UserEntity()
            {
                Name = Name,
                Email = Email,
                IsConfirmed = false,
                Id = Id,
                HashedPassword = Password,
            };
        }
    }
}