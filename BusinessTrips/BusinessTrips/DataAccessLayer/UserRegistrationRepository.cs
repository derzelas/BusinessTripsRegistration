using System;
using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRegistrationRepository : RepositoryBase<UserRegistrationModel>
    {
        public void Add(UserRegistrationModel userRegistration)
        {
            storage.Add(userRegistration);
        }

        public UserRegistrationModel Get(UserRegistrationModel userRegistration)
        {
            return storage.Get(userRegistration);
        }

        public UserRegistrationModel GetByToken(Guid requestToken)
        {
            Func<UserRegistrationModel, bool> function =
                m => m.RegisterToken == requestToken;

            return storage.Get(function);
        }
    }
}