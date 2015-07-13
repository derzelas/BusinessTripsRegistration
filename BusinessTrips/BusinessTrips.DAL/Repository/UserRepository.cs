using System;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repository
{
    public class UserRepository
    {
        private IStorage storage;

        public UserRepository()
        {
            storage = new StorageFactory().Create();
        }

        public void CreateByUserEntity(UserEntity userEntity)
        {
            storage.Add(userEntity);
        }

        public UserModel GetById(Guid id)
        {
            var userEntity = storage.GetSetFor<UserEntity>().FirstOrDefault(m => m.Id == id);

            if (userEntity == null)
            {
                return null;
            }

            return userEntity.ToModel();
        }

        public bool AreCredentialsValid(string email, string password)
        {
            return storage.GetSetFor<UserEntity>().Any(m => m.Email == email && m.HashedPassword == password && m.IsConfirmed);
        }

        public void Confirm(Guid id)
        {
            var userEntity = storage.GetSetFor<UserEntity>().Single(u => u.Id == id);
            userEntity.IsConfirmed = true;
        }

        public UserEntity GetByEmail(string email)
        {
            return storage.GetSetFor<UserEntity>().SingleOrDefault(m => m.Email == email);
        }

        public void CommitChanges()
        {
            storage.Commit();
        }
    }
}