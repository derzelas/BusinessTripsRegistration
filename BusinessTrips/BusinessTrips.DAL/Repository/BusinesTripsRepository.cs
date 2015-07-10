using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;

namespace BusinessTrips.DAL.Repository
{
    class BusinesTripsRepository : RepositoryBase
    {
        public void Add(BusinessTripModel businessTrip)
        {
            Storage.Add(businessTrip.ToEntity());
        }

        public BusinessTripModel GetById(Guid id)
        {
            return Storage.GetStorageFor<BusinessTripEntity>().Single(m => m.Id == id).ToModel();
        }

        public IEnumerable<BusinessTripModel> GetByUser(Guid id)
        {
            var result= Storage.GetStorageFor<BusinessTripEntity>().Where(m => m.User.Id == id);

            return result.Select(businessTripEntity => businessTripEntity.ToModel()).ToList();
        }

        public IEnumerable<BusinessTripModel> GetAllBusinessTripEntities(Func<BusinessTripEntity,bool> predicate)
        {
            var result = Storage.GetStorageFor<BusinessTripEntity>().Where(predicate);

            return result.Select(businessTripEntity => businessTripEntity.ToModel()).ToList();
        }
    }
}
