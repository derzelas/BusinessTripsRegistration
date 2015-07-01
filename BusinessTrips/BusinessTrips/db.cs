using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessTrips.DataAccessLayer;
using BusinessTrips.Models;

namespace BusinessTrips
{
    public static class db
    {
        public static IStorage<UserRegistrationModel> storage = new InMemoryStorage<UserRegistrationModel>();
    }
}