using System.Collections.Generic;
using System.Linq;

namespace BusinessTrips.DataAccessLayer
{
    public class InMemoryStorage<T> : IStorage<T>
    {
        private static InMemoryStorage<T> singleTone; 
        private List<T> storage;

        private InMemoryStorage(List<T> elements)
        {
            storage=elements;
        }

        public static InMemoryStorage<T> GetInstace(List<T> elements)
        {
            if (singleTone == null)
            {
                singleTone = new InMemoryStorage<T>(elements);
            }

            return singleTone;
        }

        public static InMemoryStorage<T> GetInstace()
        {
            return GetInstace(new List<T>());
        } 

        public void Add(T obj)
        {
            storage.Add(obj);
        }

        public T Get(T element)
        {
            return storage.First(e => e.Equals(element));
        }
    }
}