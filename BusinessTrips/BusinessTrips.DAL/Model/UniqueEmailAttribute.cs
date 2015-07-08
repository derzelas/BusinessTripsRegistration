using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.DAL.Model
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            UserRepository userRepository = new UserRepository();
            return !userRepository.Exists((string)value);
        }
    }
}