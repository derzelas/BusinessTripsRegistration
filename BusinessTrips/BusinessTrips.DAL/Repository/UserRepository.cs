using System;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;
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

        public UserModel GetById(Guid id)
        {
            var userEntity = storage.GetSetFor<UserEntity>().FirstOrDefault(m => m.Id == id);

            if (userEntity == null)
            {
                return null;
            }

            return userEntity.ToModel();
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

    public interface IUserRepository
    {
        void CreateByUserEntity(UserEntity userEntity);
        UserModel GetById(Guid id);
        void Confirm(Guid id);
        UserEntity GetByEmail(string email);
        void CommitChanges();
    }
}