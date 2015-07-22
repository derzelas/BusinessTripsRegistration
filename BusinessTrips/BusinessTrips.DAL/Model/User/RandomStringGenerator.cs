using System;

namespace BusinessTrips.DAL.Model.User
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        public string GetSalt()
        {
            return Guid.NewGuid().ToString();
        }
    }
}