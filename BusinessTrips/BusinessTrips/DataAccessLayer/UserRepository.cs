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
                ID = userRegistrationModel.ID,
                IsConfirmed = false
            };

            storage.Add(userModel);

            return userModel;
        }

        public UserModel GetByID(Guid id)
        {
            return storage.GetStorageFor().First(m => m.ID == id);
        }

        public bool AreCredentialsValid(string email, string password)
        {
            UserModel retrievedModel;
            try
            {
                retrievedModel = storage.GetStorageFor().First(m => m.Email == email);
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            return retrievedModel.Password == password;
        }

        public void Update(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool NotExists(string email)
        {
            try
            {
                storage.GetStorageFor().Single(m => m.Email == email);
            }
            catch (InvalidOperationException)
            {
                return true;
            }
            return false;
        }
    }
}