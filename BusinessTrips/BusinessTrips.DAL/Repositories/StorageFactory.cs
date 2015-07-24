using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repositories
{
    public class StorageFactory
    {
        private static IStorage storage;

        public StorageFactory()
        {
            if (storage == null)
            {
                storage = new EfStorage();
            }
        }

        public IStorage Create()
        {
            return storage;
        }
    }
}