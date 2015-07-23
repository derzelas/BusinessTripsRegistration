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
            FromEntity(repository.GetBy(id));
        }

        public bool Authenthicate()
        {
            var userEntity = repository.GetBy(Email);
            if (userEntity == null || !userEntity.IsConfirmed)
            {
                return false;
            }

            FromEntity(userEntity);

            var password=new Password(Password,userEntity.Salt);

            return password.GetHashed() == userEntity.HashedPassword;
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
