using System;

namespace BusinessTrips.DataAccessLayer
{
    public interface IStorage<T>
    {
        void Add(T element);

        T Get(T element);

        T Get(Predicate<T> predicate);

        void Update(T element);

        void Remove(T element);

        void Commit();
    }
}