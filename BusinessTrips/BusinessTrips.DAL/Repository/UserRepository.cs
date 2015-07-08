using System;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;

namespace BusinessTrips.DAL.Repository
{
    public class UserRepository : RepositoryBase
    {
        public void CreateByUserRegistration(UserRegistrationModel userRegistrationModel)
        {
            UserEntity userEntity = UserEntity.FromUserRegistrationModel(userRegistrationModel);

            Storage.Add(userEntity);
        }

        public UserModel GetById(Guid id)
        {
            var userEntity = Storage.GetStorageFor<UserEntity>().FirstOrDefault(m => m.Id == id);

            if (userEntity == null)
            {
                return null;
            }

            return UserModel.FromUserEntity(userEntity);
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