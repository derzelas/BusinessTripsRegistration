using System;
using System.Linq;
using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRepository : RepositoryBase<UserModel>
    {
        public UserModel CreateByUserRegistration(UserRegistrationModel userRegistrationModel)
        {
            UserModel userModel = new UserModel
            {
                Name = userRegistrationModel.Name,
                Email = userRegistrationModel.Email,
                Password = userRegistrationModel.Password,
                Id = userRegistrationModel.Id,
                IsConfirmed = false
            };

            Storage.Add(userModel);

            return userModel;
        }

        public UserModel GetById(Guid id)
        {
            return Storage.GetStorageFor().FirstOrDefault(m => m.Id == id);
        }

        public bool AreCredentialsValid(string email, string password)
        {
            return Storage.GetStorageFor().Any(m => m.Email == email && m.Password == password);
        }

        public void Update(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string email)
        {
            return Storage.GetStorageFor().Any(m => m.Email == email);
        }
    }
}