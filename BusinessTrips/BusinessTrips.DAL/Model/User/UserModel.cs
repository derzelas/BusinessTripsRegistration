using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model.BusinessTrip;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model.User
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

        public UserModel(Guid id)
        {
            FromEntity(repository.GetById(id));
        }

        private void FromEntity(UserEntity userEntity)
        {
            if (userEntity == null)
            {
                return;
            }

            Id = userEntity.Id;
            Name = userEntity.Name;
            Email = userEntity.Email;
            BusinessTrips = userEntity.BusinessTrips.ToList().Select(e => new BusinessTripModel(e));
        }

        public bool Authenthicate()
        {
            var userEntity = repository.GetByEmail(Email);
            if (userEntity == null || !userEntity.IsConfirmed)
            {
                return false;
            }

            FromEntity(userEntity);

            return PasswordHasher.GetHashed(Password + userEntity.Salt) == userEntity.HashedPassword;
        }

        public UserEntity ToEntity()
        {
            return repository.GetById(Id);
        }
    }
}
