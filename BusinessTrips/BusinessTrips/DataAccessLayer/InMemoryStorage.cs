using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTrips.DataAccessLayer
{
    public class InMemoryStorage<T>:IStorage<T>
    {
        private List<T> data;

        public InMemoryStorage()
        {
            data=new List<T>();
        }

        public void Add(T obj)
        {
            data.Add(obj);
        }
    }
}