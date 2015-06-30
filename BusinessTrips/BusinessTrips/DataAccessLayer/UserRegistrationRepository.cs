using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRegistrationRepository
    {
        private IStorage<UserRegistrationModels> storage;

        public UserRegistrationRepository(IStorage<UserRegistrationModels> storage)
        {
            this.storage = storage;
        }

        public void Add(UserRegistrationModels userRegistration)
        {
            storage.Add(userRegistration);
        }
    }
}