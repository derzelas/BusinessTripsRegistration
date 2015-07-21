using System;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Exception;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
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
                throw new UserNotFoundInDataBaseException();
            }

            if (userEntity.IsConfirmed)
            {
                return;
            }

            userRepository.ConfirmRegistration(userEntity.Id);
                userRepository.CommitChanges();
            }
        }
    }
