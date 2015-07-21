using System;

namespace BusinessTrips.DAL.Model
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        public string GetSalt()
        {
            return Guid.NewGuid().ToString();
        }
    }
}