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

        public override void CommitChanges()
        {
            throw new System.NotImplementedException();
        }

        public UserRegistrationModel GetByToken(Guid requestToken)
        {
            throw new NotImplementedException();
        }
    }
}