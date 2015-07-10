using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repository
{
    class BusinesTripsRepository
    {
        private IStorage storage;

        public BusinesTripsRepository()
        {
            storage = new StorageFactory().Create();
        }

        public void CommitChanges()
        {
            storage.Commit();
        }

        public void Add(BusinessTripModel businessTrip)
        {
            storage.Add(businessTrip.ToEntity());
        }

        public BusinessTripModel GetById(Guid id)
        {
            return storage.GetSetFor<BusinessTripEntity>().Single(m => m.Id == id).ToModel();
        }

        public IEnumerable<BusinessTripModel> GetByUser(Guid id)
        {
            var result= storage.GetSetFor<BusinessTripEntity>().Where(m => m.User.Id == id);

            return result.Select(businessTripEntity => businessTripEntity.ToModel()).ToList();
        }
    }
}
