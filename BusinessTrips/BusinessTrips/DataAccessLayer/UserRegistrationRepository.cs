using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRegistrationRepository
    {
        private IStorage<UserRegistrationModel> storage;

        public UserRegistrationRepository()
        {
            storage = new InMemoryStorage<UserRegistrationModel>();
        }

        public void Add(UserRegistrationModel userRegistration)
        {
            storage.Add(userRegistration);
        }

        public UserRegistrationModel Get(UserRegistrationModel userRegistration)
        {
            return storage.Get(userRegistration);
        }
    }
}