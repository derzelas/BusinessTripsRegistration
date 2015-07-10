using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repository
{
    public class BusinesTripsRepository
    {
        private IStorage storage;

        public BusinesTripsRepository()
        {
            storage = new StorageFactory().Create();
        }

        public void Add(BusinessTripModel businessTripModel)
        {
            storage.Add(businessTripModel.ToEntity());
        }

        public BusinessTripModel GetById(Guid id)
        {
            return (storage.GetSetFor<BusinessTripEntity>().First(m => m.Id == id)).ToModel();
        }

        public IEnumerable<BusinessTripModel> GetByUser(Guid id)
        {
            var result = storage.GetSetFor<BusinessTripEntity>().Where(m => m.User.Id == id);

            List<BusinessTripModel> list = new List<BusinessTripModel>();
            foreach (var entity in result)
            {
                list.Add(entity.ToModel());
            }
            return list;
        }

        public void CommitChanges()
        {
            storage.Commit();
        }
    }
}
