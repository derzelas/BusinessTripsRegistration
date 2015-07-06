using System.Linq;

namespace BusinessTrips.DataAccessLayer
{
    public interface IStorage<T>
    {
        void Add(T element);

        IQueryable<T> GetStorageFor();

        void Update(T element);

        void Remove(T element);

        void Commit();
    }
}