using System;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Exception;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model.User
{
    public class RegistrationConfirmationModel
    {
        public Guid Id { get; set; }

        public void Confirm()
        {
            var userRepository = new UserRepository();
            UserEntity userEntity;

            try
            {
                userEntity = userRepository.GetById(Id);
            }
            catch (InvalidOperationException)
            {
                throw new UserNotFoundException();
            }

            if (userEntity.IsConfirmed)
            {
                return;
            }

            userEntity.IsConfirmed = true;
            userRepository.CommitChanges();
        }
    }
}
