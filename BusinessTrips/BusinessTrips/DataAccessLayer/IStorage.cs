using System;

namespace BusinessTrips.DataAccessLayer
{
    public interface IStorage<T>
    {
        void Add(T element);

        T Get(T element);

        T Get(Func<T,bool> predicate);

        void Update(T element);

        void Remove(T element);

        void Commit();
    }
}