using System;
using BusinessTrips.DataAccessLayer;

namespace BusinessTrips.Models
{
    public class RegistrationConfirmationModel
    {

        public Guid RequestToken { get; set; }

        public void Confirm()
        {
            UserRegistrationRepository userRegistrationRepository=new UserRegistrationRepository();

            UserRegistrationModel userRegistrationModel = userRegistrationRepository.GetByToken(RequestToken);

            UserRepository userRepository=new UserRepository();

            userRepository.CreateByUserRegistration(userRegistrationModel);
        }
    }
}