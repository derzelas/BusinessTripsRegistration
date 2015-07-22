using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repository
{
    public class RoleRepository
    {
        private readonly IStorage storage;

        public RoleRepository()
        {
            storage = new StorageFactory().Create();
        }

        public RoleEntity GetRole(Role roleName)
        {
            return storage.GetStorageFor<RoleEntity>().Single(r => r.Name == roleName.ToString());
        }
    }
}
