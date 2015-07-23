using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Attribute
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var userRepository = new UserRepository();

            return userRepository.GetBy((string)value) == null;
        }
    }
}