using System;
using System.Collections.Generic;
using BusinessTrips.DAL.Model;

namespace BusinessTrips.DAL.Entity
{
    public class UserEntity
    {
        public UserEntity()
        {
            Roles = new HashSet<Role>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsConfirmed { get; set; }
        public virtual ICollection<Role> Roles {get; set; }

        public UserModel ToModel()
        {
            return new UserModel
            {
                Name = Name,
                Email = Email,
                IsConfirmed = IsConfirmed,
                Id = Id,
                Password = HashedPassword
            };
        }
    }
}