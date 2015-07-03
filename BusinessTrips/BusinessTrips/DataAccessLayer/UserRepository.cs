using System;
using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class User : RepositoryBase<UserModel>
    {
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

            return retrievedModel.Password == password;
        }

        public UserModel CreateByUserRegistration(UserRegistrationModel userRegistrationModel)
        {
            UserModel userModel = new UserModel();
            userModel.Name = userRegistrationModel.Name;
            userModel.Email = userRegistrationModel.Email;
            userModel.Password = userRegistrationModel.Password;

            storage.Add(userModel);

            return userModel;
        }

        public override void CommitChanges()
        {
            throw new NotImplementedException();
        }
    }
}