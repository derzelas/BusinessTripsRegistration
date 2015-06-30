using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRegistrationStorage
    {
        private IStorage<UserRegistrationModel> storage;

        public UserRegistrationStorage()
        {
            storage= new InMemoryStorage<UserRegistrationModel>();
        }

        public void Add(UserRegistrationModel userRegistration)
        {
            storage.Add(userRegistration);
        }
    }
}