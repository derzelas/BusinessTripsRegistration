using System;
using System.Collections.Generic;

namespace BusinessTrips.DAL.Entities
{
    public class UserEntity
    {
        public UserEntity()
        {
            Roles = new HashSet<RoleEntity>();
            BusinessTrips = new HashSet<BusinessTripEntity>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsConfirmed { get; set; }
        public virtual ICollection<RoleEntity> Roles { get; set; }
        public virtual ICollection<BusinessTripEntity> BusinessTrips { get; set; }
    }
}