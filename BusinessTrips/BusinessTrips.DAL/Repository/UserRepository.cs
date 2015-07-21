using System;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private IStorage storage;

        public UserRepository()
        {
            storage = new StorageFactory().Create();
        }

        public UserRepository(IStorage storage)
        {
            this.storage = storage;
        }

        public void CreateByUserEntity(UserEntity userEntity)
        {
            storage.Add(userEntity);
        }

        public UserEntity GetById(Guid id)
        {
            return storage.GetSetFor<UserEntity>().First(m => m.Id == id);
        }

        public UserEntity GetByEmail(string email)
        {
            return storage.GetSetFor<UserEntity>().Single(m => m.Email == email);
        }

        public void Confirm(Guid id)
        {
            var userEntity = storage.GetSetFor<UserEntity>().Single(u => u.Id == id);
            userEntity.IsConfirmed = true;
        }

        public void CommitChanges()
        {
            storage.Commit();
        }
    }

    public interface IUserRepository
    {
        void CreateByUserEntity(UserEntity userEntity);
        UserEntity GetById(Guid id);
        void Confirm(Guid id);
        UserEntity GetByEmail(string email);
        void CommitChanges();
    }
}