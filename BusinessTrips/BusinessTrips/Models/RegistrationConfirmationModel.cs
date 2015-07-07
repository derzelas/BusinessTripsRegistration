using System;
using BusinessTrips.DataAccessLayer;

namespace BusinessTrips.Models
{
    public class RegistrationConfirmationModel
    {
        public Guid Id { get; set; }

        public bool Confirm()
        {
            UserRepository userRepository = new UserRepository();

            UserModel userModel = userRepository.GetById(Id);

            if (userModel.IsConfirmed == false)
            {
                userModel.IsConfirmed = true;
                userRepository.Update(userModel);

                return true;
            }
            return false;
        }
    }
}