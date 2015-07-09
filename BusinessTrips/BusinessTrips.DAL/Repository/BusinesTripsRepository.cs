using System;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;

namespace BusinessTrips.DAL.Repository
{
    class BusinesTripsRepository : RepositoryBase
    {
        public void Add(BusinessTripModel businessTrip)
        {
            Storage.Add(businessTrip);
        }

        public BusinessTripEntity GetById(Guid id)
        {
           return Storage.GetStorageFor<BusinessTripEntity>().Single(m => m.Id == id);
        }
    }
}
