using System;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Exception;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IStorage storage;

        public UserRepository()
        {
            storage = new StorageFactory().Create();
        }

        public void CreateByUserEntity(UserEntity userEntity)
        {
            storage.Add(userEntity);
        }

        public UserEntity GetById(Guid userId)
        {
            UserEntity userEntity = storage.GetStorageFor<UserEntity>().Single(m => m.Id == userId);

            if (userEntity == null)
            {
                throw new UserNotFoundException();
            }

            return userEntity;
        }

        public UserEntity GetByEmail(string userEmail)
        {
            return storage.GetStorageFor<UserEntity>().SingleOrDefault(m => m.Email == userEmail);
        }

        public void CommitChanges()
        {
            storage.Commit();
        }
    }
}