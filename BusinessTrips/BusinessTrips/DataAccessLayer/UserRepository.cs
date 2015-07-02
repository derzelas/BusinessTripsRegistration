using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRepository
    {
        private IStorage<UserModel> storage;

        public UserRepository()
        {
            storage = new InMemoryStorage<UserModel>();
        }

        public bool AreCredentialsValid(string email, string password)
        {
            UserModel userModel = new UserModel();
            userModel.Email = email;

            UserModel retrivedModel = storage.Get(userModel);

            if (retrivedModel.Password == password)
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