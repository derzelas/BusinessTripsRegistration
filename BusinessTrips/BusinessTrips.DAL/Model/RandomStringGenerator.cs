using System;

namespace BusinessTrips.DAL.Model
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        public string GetString()
        {
            return Guid.NewGuid().ToString();
        }
    }

    public interface IRandomStringGenerator
    {
        string GetString();
    }
}