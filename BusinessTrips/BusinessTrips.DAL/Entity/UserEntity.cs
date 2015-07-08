using System;
using BusinessTrips.DAL.Model;

namespace BusinessTrips.DAL.Entity
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public bool IsConfirmed { get; set; }

        public static UserEntity FromUserModel(UserModel userModel)
        {
            return new UserEntity()
            {
                Name = userModel.Name,
                Email = userModel.Email,
                IsConfirmed = userModel.IsConfirmed,
                Id = userModel.Id,
                Password = userModel.Password
            };
        }

        public static UserEntity FromUserRegistrationModel(UserRegistrationModel userRegistrationModel)
        {
            return new UserEntity()
            {
                Name = userRegistrationModel.Name,
                Email = userRegistrationModel.Email,
                IsConfirmed = false,
                Id = userRegistrationModel.Id,
                Password = userRegistrationModel.Password
            };
        }
    }
}