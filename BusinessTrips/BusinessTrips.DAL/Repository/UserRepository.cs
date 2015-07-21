using System;
using System.Linq;
using BusinessTrips.DAL.Entity;
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
            return storage.GetStorageFor<UserEntity>().Single(m => m.Id == userId);
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