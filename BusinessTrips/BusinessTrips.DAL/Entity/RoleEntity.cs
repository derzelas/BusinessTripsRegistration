﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessTrips.DAL.Entity
{
    public class RoleEntity
    {
        public RoleEntity()
        {
            Users = new HashSet<UserEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; } 
    }
}