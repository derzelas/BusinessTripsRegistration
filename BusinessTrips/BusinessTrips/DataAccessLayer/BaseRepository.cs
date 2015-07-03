namespace BusinessTrips.DataAccessLayer
{
    public abstract class RepositoryBase<T>
    {
        protected IStorage<T> storage;
        public abstract void CommitChanges();
    }
}