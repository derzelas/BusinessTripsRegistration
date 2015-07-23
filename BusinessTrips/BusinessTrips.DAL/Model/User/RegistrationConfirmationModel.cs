using System;
using BusinessTrips.DAL.Exception;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model.User
{
    public class RegistrationConfirmationModel
    {
        public void Confirm(string id)
        {
            Validate(id);

            var userRepository = new UserRepository();

            var userEntity = userRepository.GetBy(Guid.Parse(id));

            if (userEntity.IsConfirmed)
            {
                return;
            }

            userEntity.IsConfirmed = true;
            userRepository.SaveChanges();
        }

        private static void Validate(string id)
        {
            Guid parsed;
            if (!Guid.TryParse(id, out parsed))
            {
                throw new InvalidIdException();
            }
        }
    }
}
