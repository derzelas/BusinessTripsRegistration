namespace BusinessTrips.DataAccessLayer
{
    public abstract class RepositoryBase<T>
    {
        protected IStorage<T> Storage;

        protected RepositoryBase()
        {
            Storage = new InMemoryStorage<T>();
        }

        public void CommitChanges()
        {
            Storage.Commit();
        }
    }
}