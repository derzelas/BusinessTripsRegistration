using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsConfirmed { get; set; }

        public UserModel()
        {
        }

        public UserModel(UserEntity userEntity)
        {
            Id = userEntity.Id;
            Name = userEntity.Name;
            Email = userEntity.Email;
            Password = userEntity.HashedPassword;
            IsConfirmed = userEntity.IsConfirmed;
        }

        public bool Authenthicate()
        {
            var repository = new UserRepository();
            UserEntity userEntity = repository.GetByEmail(Email);

            if (userEntity == null || userEntity.IsConfirmed == false)
            {
                return false;
            }

            string hashPassword = PasswordHasher.HashPassword(Password + userEntity.Salt);
            return hashPassword == userEntity.HashedPassword;
        }
    }
}
