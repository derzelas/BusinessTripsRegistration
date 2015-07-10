using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;

namespace BusinessTrips.DAL.Repository
{
    public class BusinesTripsRepository : RepositoryBase
    {
        public void Add(BusinessTripModel businessTrip)
        {
            Storage.Add(businessTrip);
        }

        public BusinessTripEntity GetById(Guid id)
        {
            return Storage.GetStorageFor<BusinessTripEntity>().Single(m => m.Id == id);
        }

        public IEnumerable<BusinessTripEntity> GetByUser(Guid id)
        {
            return Storage.GetStorageFor<BusinessTripEntity>().Where(m => m.User.Id == id);
        }

        public IEnumerable<BusinessTripEntity> GetAllBusinessTripEntities(Func<BusinessTripEntity,bool> predicate)
        {
            return Storage.GetStorageFor<BusinessTripEntity>().Where(predicate);
        }
    }
}
