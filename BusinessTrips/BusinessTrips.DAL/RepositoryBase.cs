namespace BusinessTrips.DAL
{
    public abstract class RepositoryBase
    {
        protected static IStorage Storage;

        protected RepositoryBase()
        {
            Storage = new EfStorage();
        }

        public void CommitChanges()
        {
            Storage.Commit();
        }
    }
}