namespace BusinessTrips.DataAccessLayer
{
    public abstract class RepositoryBase<T>
    {
        protected IStorage<T> storage;

        protected RepositoryBase()
        {
            storage = new InMemoryStorage<T>();
        }

        public void CommitChanges()
        {
            storage.Commit();
        }
    }
}