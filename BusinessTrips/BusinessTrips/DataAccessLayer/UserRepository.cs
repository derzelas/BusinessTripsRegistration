using System;
using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRepository
    {
        private IStorage<UserModel> storage;

        public UserRepository(IStorage<UserModel> storage)
        {
            this.storage = storage;
        }

        public bool AreCredentialsValid(string email, string password)
        {
            UserModel userModel = new UserModel();
            userModel.Email = email;

            UserModel retrievedModel;
            try
            {
                retrievedModel = storage.Get(userModel);
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            if (retrievedModel.Password == password)
                return true;
            return false;
        }

        public UserModel CreateByUserRegistration(UserRegistrationModel userRegistrationModel)
        {
            UserModel userModel = new UserModel();
            userModel.Name = userRegistrationModel.Name;
            userModel.Email = userRegistrationModel.Email;
            userModel.Password = userRegistrationModel.Password;
            
            return userModel;
        }
    }
}