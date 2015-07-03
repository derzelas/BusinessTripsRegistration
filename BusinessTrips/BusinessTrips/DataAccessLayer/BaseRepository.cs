namespace BusinessTrips.DataAccessLayer
{
    public abstract class BaseRepository<T>
    {
        protected IStorage<T> storage;
        public abstract void CommitChanges();
    }
}