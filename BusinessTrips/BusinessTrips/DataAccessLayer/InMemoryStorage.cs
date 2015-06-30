using System.Collections.Generic;
using System.Linq;

namespace BusinessTrips.DataAccessLayer
{
    public class InMemoryStorage<T> : IStorage<T>
    {
        private List<T> storage;

        public InMemoryStorage()
        {
            storage = new List<T>();
        }

        public InMemoryStorage(List<T> elements)
        {
            storage = elements;
        }

        public void Add(T obj)
        {
            storage.Add(obj);
        }

        public T Get(T element)
        {
            return storage.First(e=>e.Equals(element));
        }
    }
}