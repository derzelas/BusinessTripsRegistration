using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
{
    public class UserModel
    {
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

        public bool IsConfirmed { get; set; }

        public IEnumerable<BusinessTripModel> BusinessTrips { get; set; } 

        public UserModel()
        {
            BusinessTrips = new List<BusinessTripModel>();
        }

        public UserModel(UserEntity userEntity)
        {
            Load(userEntity);
        }

        public UserModel(Guid id)
        {
            var repository = new UserRepository();
            Load(repository.GetById(id));       
        }

        private void Load(UserEntity userEntity)
        {
            if (userEntity == null)
            {
                return;
            }

            Id = userEntity.Id;
            Name = userEntity.Name;
            Email = userEntity.Email;
            Password = userEntity.HashedPassword;
            IsConfirmed = userEntity.IsConfirmed;
            BusinessTrips = userEntity.BusinessTrips.ToList().Select(e => new BusinessTripModel(e));
        }

        public bool Authenthicate()
        {
            var repository = new UserRepository();
            UserEntity userEntity = repository.GetByEmail(Email);

            if (userEntity == null || userEntity.IsConfirmed == false)
            {
                return false;
            }

            string hashPassword = PasswordHasher.HashPassword(Password + userEntity.Salt);
            Load(userEntity);

            return hashPassword == userEntity.HashedPassword;
        }

        public UserEntity ToEntity()
        {
            var repository = new UserRepository();

            return repository.GetById(Id);
        }
    }
}
