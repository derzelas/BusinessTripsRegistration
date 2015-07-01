using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRegistrationRepository
    {
        private IStorage<UserRegistrationModel> storage;

        public UserRegistrationRepository(IStorage<UserRegistrationModel> storage)
        {
            this.storage = storage;
        }

        public void Add(UserRegistrationModel userRegistration)
        {
            storage.Add(userRegistration);
        }
    }
}