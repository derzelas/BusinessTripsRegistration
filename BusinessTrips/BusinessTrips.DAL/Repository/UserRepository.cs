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
            UserEntity userEntity = userRegistrationModel.ToUserEntity();

            Storage.Add(userEntity);
        }

        public UserModel GetById(Guid id)
        {
            var userEntity = Storage.GetStorageFor<UserEntity>().FirstOrDefault(m => m.Id == id);

            if (userEntity == null)
            {
                return null;
            }

            return userEntity.ToUserModel();
        }

        public bool AreCredentialsValid(string email, string password)
        {
            return Storage.GetStorageFor<UserEntity>().Any(m => m.Email == email && m.Password == password && m.IsConfirmed);
        }

        public void Confirm(Guid id)
        {
            var userEntity = Storage.GetStorageFor<UserEntity>().Single(u => u.Id == id);
            userEntity.IsConfirmed = true;
        }

        public bool Exists(string email)
        {
            return Storage.GetStorageFor<UserEntity>().Any(m => m.Email == email);
        }
    }
}