using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Exceptions;
using BusinessTrips.DAL.Repositories;

namespace BusinessTrips.DAL.Models.User
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
