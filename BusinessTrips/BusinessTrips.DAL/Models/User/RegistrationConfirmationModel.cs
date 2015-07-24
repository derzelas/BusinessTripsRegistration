using System;
using BusinessTrips.DAL.Exceptions;
using BusinessTrips.DAL.Repositories;

namespace BusinessTrips.DAL.Models.User
{
    public class RegistrationConfirmationModel
    {
        public void Confirm(string userId)
        {
            var userRepository = new UserRepository();

            var userEntity = userRepository.GetBy(GetGuidBy(userId));

            if (userEntity.IsConfirmed)
            {
                return;
            }

            userEntity.IsConfirmed = true;
            userRepository.SaveChanges();
        }

        private static Guid GetGuidBy(string userId)
        {
            Guid parsedId;

            if (!Guid.TryParse(userId, out parsedId))
            {
                throw new InvalidIdException();
            }

            return parsedId;
        }
    }
}
