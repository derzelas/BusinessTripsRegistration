﻿using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repository
{
    public class RoleRepository
    {
        private IStorage storage;

        public RoleRepository()
        {
            storage = new StorageFactory().Create();
        }

        public RoleEntity GetRole(string roleName)
        {
            return storage.GetSetFor<RoleEntity>().FirstOrDefault(r => r.Name == roleName);
        }
    }
}