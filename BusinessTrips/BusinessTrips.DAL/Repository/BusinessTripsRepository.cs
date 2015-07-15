using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repository
{
    public class BusinessTripsRepository
    {
        private IStorage storage;

        public BusinessTripsRepository()
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
            var result = storage.GetSetFor<BusinessTripEntity>().Where(m => m.UserId == id);

            List<BusinessTripModel> list = new List<BusinessTripModel>();
            foreach (var entity in result)
            {
                list.Add(entity.ToModel());
            }

            return list;
        }

        public void UpdateStatus(Guid id, string status)
        {
            var businessTripEntity = storage.GetSetFor<BusinessTripEntity>().Single(u => u.Id == id);
            businessTripEntity.Status = status;
        }

        public void CommitChanges()
        {
            storage.Commit();
        }
    }
}
