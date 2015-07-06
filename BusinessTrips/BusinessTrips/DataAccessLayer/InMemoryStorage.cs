using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessTrips.DataAccessLayer
{
    public class InMemoryStorage<T> : IStorage<T>
    {
        private List<T> storage = new List<T>();

        public void Add(T obj)
        {
            storage.Add(obj);
        }

        public IQueryable<T> GetStorageFor()
        {
            return storage.AsQueryable();
        }

        public void Update(T element)
        {
            throw new NotImplementedException();
        }

        public void Remove(T element)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}