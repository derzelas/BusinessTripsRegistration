using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRegistration : RepositoryBase<UserRegistrationModel>
    {
        public void Add(UserRegistrationModel userRegistration)
        {
            storage.Add(userRegistration);
        }

        public UserRegistrationModel Get(UserRegistrationModel userRegistration)
        {
            return storage.Get(userRegistration);
        }

        public override void CommitChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}