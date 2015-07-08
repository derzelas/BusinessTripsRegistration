using System;

namespace BusinessTrips.DAL.Model
{
    public class RegistrationConfirmationModel
    {
        public Guid Id { get; set; }

        public void Confirm()
        {
            UserRepository userRepository = new UserRepository();

            UserModel userModel = userRepository.GetById(Id);

            if (userModel.IsConfirmed == false)
            {
                userRepository.Confirm(userModel);
            }
        }
    }
}