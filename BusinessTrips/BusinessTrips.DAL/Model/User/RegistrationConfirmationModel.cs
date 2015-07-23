using System;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model.User
{
    public class RegistrationConfirmationModel
    {
        public Guid Id { get; set; }

        public void Confirm()
        {
            var userRepository = new UserRepository();

            var userEntity = userRepository.GetBy(Id);

            if (userEntity.IsConfirmed)
            {
                return;
            }

            userEntity.IsConfirmed = true;
            userRepository.SaveChanges();
        }
    }
}
