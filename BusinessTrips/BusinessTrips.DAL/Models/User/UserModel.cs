using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Models.BusinessTrip;
using BusinessTrips.DAL.Repositories;

namespace BusinessTrips.DAL.Models.User
{
    public class UserModel
    {
        private readonly UserRepository repository = new UserRepository();
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IEnumerable<BusinessTripModel> BusinessTrips { get; set; }

        public UserModel()
        {
            BusinessTrips = new List<BusinessTripModel>();
        }

        public UserModel(UserEntity userEntity)
        {
            FromEntity(userEntity);
        }

        public UserModel(Guid userId)
        {
            FromEntity(repository.GetBy(userId));
        }

        public bool Authenthicate()
        {
            var userEntity = repository.GetBy(Email);
            if (userEntity == null || !userEntity.IsConfirmed)
            {
                return false;
            }

            FromEntity(userEntity);

            var password = new Password(Password, userEntity.Salt);

            return userEntity.HashedPassword == password.GetHashed();
        }

        public UserEntity GetEntity()
        {
            return repository.GetBy(Id);
        }

        private void FromEntity(UserEntity userEntity)
        {
            Id = userEntity.Id;
            Name = userEntity.Name;
            Email = userEntity.Email;
            BusinessTrips = userEntity.BusinessTrips.ToList().Select(e => new BusinessTripModel(e));
        }
    }
}
