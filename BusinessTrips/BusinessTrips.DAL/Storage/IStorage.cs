using System.Linq;

namespace BusinessTrips.DAL.Storage
{
    public interface IStorage
    {
        void Add<T>(T element) where T : class;

        IQueryable<T> GetSetFor<T>() where T : class;

        void Commit();
    }
}