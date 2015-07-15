using System.Collections.Generic;

namespace BusinessTrips.DAL.Entity
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<UserEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; } 
    }
}
