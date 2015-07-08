using System;

namespace BusinessTrips.DAL
{
    public abstract class RepositoryBase : IDisposable
    {
        protected static IStorage Storage;
        private bool disposed;

        protected RepositoryBase()
        {
            Storage = new EfStorage();
        }

        public void CommitChanges()
        {
            Storage.Commit();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                if (Storage != null)
                {
                    Storage.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}