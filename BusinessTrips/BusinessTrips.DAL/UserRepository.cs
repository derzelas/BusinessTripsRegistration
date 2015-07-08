using System;
using System.Linq;
using BusinessTrips.DAL.Model;

namespace BusinessTrips.DAL
{
    public class UserRepository : RepositoryBase
    {
        public void CreateByUserRegistration(UserRegistrationModel userRegistrationModel)
        {
            UserEntity userEntity = new UserEntity
            {
                Name = userRegistrationModel.Name,
                Email = userRegistrationModel.Email,
                Password = userRegistrationModel.Password,
                Id = userRegistrationModel.Id,
                IsConfirmed = false
            };

            Storage.Add(userEntity);
        }

        public UserModel GetById(Guid id)
        {
            var userEntity = Storage.GetStorageFor<UserEntity>().FirstOrDefault(m => m.Id == id);

            if (userEntity == null) return null;

            return new UserModel
            {
                Name = userEntity.Name,
                Email = userEntity.Email,
                Password = userEntity.Password,
                Id = userEntity.Id,
                IsConfirmed = userEntity.IsConfirmed
            };
        }

        public bool AreCredentialsValid(string email, string password)
        {
            return Storage.GetStorageFor<UserEntity>().Any(m => m.Email == email && m.Password == password);
        }

        public void Confirm(UserModel userModel)
        {
            var userEntity = Storage.GetStorageFor<UserEntity>().Single(u => u.Id == userModel.Id);
            userEntity.IsConfirmed = true;
        }

        public bool Exists(string email)
        {
            return Storage.GetStorageFor<UserEntity>().Any(m => m.Email == email);
        }
    }
}