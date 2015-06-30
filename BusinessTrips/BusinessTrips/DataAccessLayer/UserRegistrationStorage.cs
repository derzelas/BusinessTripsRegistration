using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRegistrationStorage
    {
        private IStorage<UserRegistrationModels> storage;

        public UserRegistrationStorage()
        {
            storage= new InMemoryStorage<UserRegistrationModels>();
        }

        public void Add(UserRegistrationModels userRegistration)
        {
            storage.Add(userRegistration);
        }
    }
}