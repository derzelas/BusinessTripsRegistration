using System;
using System.Linq;
using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Exceptions;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repositories
{
    public class UserRepository
    {
        private readonly IStorage storage;

        public UserRepository()
        {
            storage = new StorageFactory().Create();
        }

        public void Add(UserEntity userEntity)
        {
            storage.Add(userEntity);
        }

        public UserEntity GetBy(Guid userId)
        {
            UserEntity userEntity = storage.GetStorageFor<UserEntity>().SingleOrDefault(m => m.Id == userId);

            if (userEntity == null)
            {
                throw new UserNotFoundException();
            }

            return userEntity;
        }

        public UserEntity GetBy(string userEmail)
        {
            return storage.GetStorageFor<UserEntity>().SingleOrDefault(m => m.Email == userEmail);
        }

        public void SaveChanges()
        {
            storage.SaveChanges();
        }
    }
}