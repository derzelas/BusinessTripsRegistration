using System.ComponentModel.DataAnnotations;
using BusinessTrips.DataAccessLayer;

namespace BusinessTrips.Models
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            UserRepository userRepository = new UserRepository();
            return userRepository.NotExists((string)value);
        }
    }
}