namespace BusinessTrips.DataAccessLayer
{
    public abstract class RepositoryBase<T>
    {
        protected IStorage<T> storage;
        public abstract void CommitChanges();

        protected RepositoryBase()
        {
            storage = new InMemoryStorage<T>();
        }
    }
}