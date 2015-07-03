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
            Predicate<UserRegistrationModel> predicate =
                userRegistrationModel => userRegistrationModel.RegisterToken.Equals(requestToken);

            return storage.Get(predicate);
        }
    }
}