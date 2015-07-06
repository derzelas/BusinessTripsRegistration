using System;
using BusinessTrips.DataAccessLayer;

namespace BusinessTrips.Models
{
    public class RegistrationConfirmationModel
    {

        public Guid ID { get; set; }

        public void Confirm()
        {
            UserRepository userRepository = new UserRepository();

            UserModel userModel = userRepository.GetByID(ID);

            userModel.IsConfirmed = true;

            userRepository.Update(userModel);
        }
    }
}