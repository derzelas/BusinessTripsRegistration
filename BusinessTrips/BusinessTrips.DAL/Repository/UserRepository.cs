using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        public void CreateByUserRegistration(UserRegistrationModel userRegistrationModel)
        {
            UserEntity userEntity = userRegistrationModel.ToUserEntity();
            userEntity.Salt = Guid.NewGuid().ToString();
            userEntity.Password = HashPassword(userEntity.Password + userEntity.Salt);
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
            UserEntity userEntity = storage.GetSetFor<UserEntity>().SingleOrDefault(m => m.Email == email && m.IsConfirmed);
            
            if (userEntity == null)
            {
                return false;
            }

            return userEntity.Password == HashPassword(password + userEntity.Salt);
        }

        public void Confirm(Guid id)
        {
            var userEntity = storage.GetSetFor<UserEntity>().Single(u => u.Id == id);
            userEntity.IsConfirmed = true;
        }

        public bool Exists(string email)
        {
            return storage.GetSetFor<UserEntity>().Any(m => m.Email == email);
        }

        public void CommitChanges()
        {
            storage.Commit();
        }

        private static string HashPassword(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte bit in crypto)
            {
                hash.Append(bit.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}