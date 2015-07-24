using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Attributes;
using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Repositories;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Models.User
{
    public class UserRegistrationModel
    {
        private const int MinimumNameLength = 3;
        private const int MinimumPasswordLength = 6;

        private readonly UserRepository userRepository;

        public Guid Id { get; private set; }

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
        [MinLength(MinimumPasswordLength, ErrorMessage = "Minimum password length is 6")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match confirmation password.")]
        [Display(Name = "Confirm password")]
        public string ConfirmedPassword { get; set; }

        public UserRegistrationModel()
        {
            userRepository = new UserRepository();
            Id = Guid.NewGuid();
        }

        public void Save()
        {
            UserEntity userEntity = ToUserEntity();

            userRepository.Add(userEntity);
            userRepository.SaveChanges();
        }

        private UserEntity ToUserEntity()
        {
            var password = new Password(Password);

            var userEntity = new UserEntity
            {
                Name = Name,
                Email = Email,
                IsConfirmed = false,
                Id = Id,
                Salt = password.GetSalt(),
                HashedPassword = password.GetHashed(),
            };

            userEntity.Roles.Add(new RoleRepository().GetRole(Role.Regular));

            return userEntity;
        }
    }
}