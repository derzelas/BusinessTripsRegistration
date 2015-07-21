using System;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
{
    public class RegistrationConfirmationModel
    {
        public Guid Id { get; set; }

        public void Confirm()
        {
            var userRepository = new UserRepository();
            UserEntity userEntity = userRepository.GetById(Id);

            if (userEntity == null)
            {
                return;
            }

            UserModel userModel = new UserModel(userEntity);

            if (userModel.IsConfirmed == false)
            {
                userRepository.ConfirmRegistration(userModel.Id);
                userRepository.CommitChanges();
            }
        }
    }
}