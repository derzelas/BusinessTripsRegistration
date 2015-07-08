using System;
using System.Linq;

namespace BusinessTrips.DAL
{
    public interface IStorage : IDisposable
    {
        void Add<T>(T element) where T : class;

        IQueryable<T> GetStorageFor<T>() where T : class;

        void Remove<T>(T element) where T : class;

        void Commit();
    }
}