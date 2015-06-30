using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTrips.DataAccessLayer
{
    interface IStorage<T>
    {
        void Add(T obj);
    }
}
