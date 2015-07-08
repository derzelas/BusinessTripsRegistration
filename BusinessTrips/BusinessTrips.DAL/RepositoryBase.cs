namespace BusinessTrips.DAL
{
    public abstract class RepositoryBase
    {
        protected static IStorage Storage;

        protected RepositoryBase()
        {
            Storage = new EFStorage();
        }

        public void CommitChanges()
        {
            Storage.Commit();
        }
    }
}