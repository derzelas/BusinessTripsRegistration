using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model.User
{
    public class ForgotPasswordModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        public ForgotPasswordModel ToForgotPasswordModelBy(string email)
        {
            var userRepository = new UserRepository();

            UserEntity userEntity = userRepository.GetBy(email);

            if (userEntity != null)
            {
                Id = userEntity.Id;
                return this;
            }

            return null;
        }
    }
}
