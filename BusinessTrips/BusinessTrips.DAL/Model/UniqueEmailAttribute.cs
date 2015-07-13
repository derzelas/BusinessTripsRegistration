using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            UserRepository userRepository = new UserRepository();
            return userRepository.GetByEmail((string)value) == null;
        }
    }
}