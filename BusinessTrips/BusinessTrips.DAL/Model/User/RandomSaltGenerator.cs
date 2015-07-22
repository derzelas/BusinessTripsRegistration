using System;

namespace BusinessTrips.DAL.Model.User
{
    public class RandomSaltGenerator : IRandomSaltGenerator
    {
        public string GetSalt()
        {
            return Guid.NewGuid().ToString();
        }
    }
}