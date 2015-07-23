using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Exception;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model.User
{
    public class RecoverPasswordModel
    {
        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        public Guid GetId()
        {
            var userRepository = new UserRepository();
            UserEntity userEntity = userRepository.GetBy(Email);

            if (userEntity == null)
            {
                throw new UserNotFoundException();                
            }

            return userEntity.Id;
        }
    }
}
