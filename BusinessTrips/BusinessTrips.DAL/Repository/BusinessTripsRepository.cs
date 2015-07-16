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
            return storage.GetSetFor<BusinessTripEntity>().Where(e => e.UserId == id).ToList().Select(e => e.ToModel());
        }

        public IEnumerable<BusinessTripModel> GetOtherBusinessTrips(BusinessTripFilter filter)
        {
            var queryable = storage.GetSetFor<BusinessTripEntity>();
            var queryEmployeeName = storage.GetSetFor<UserEntity>();

            if (!string.IsNullOrEmpty(filter.EmployeeName))
            {
                queryEmployeeName = queryEmployeeName.Where(m => m.Name.Contains(filter.EmployeeName));
                queryEmployeeName.ToList().Select(b => b.ToModel());
                foreach (var bla in queryEmployeeName)
                {
                    queryable = queryable.Where(m => m.UserId == bla.Id);  
                }                
            }

            if (!string.IsNullOrEmpty(filter.ClientName))
            {
                queryable = queryable.Where(m => m.ClientName.Contains(filter.ClientName));
            }

            if (!string.IsNullOrEmpty(filter.Location))
            {
                queryable = queryable.Where(m => m.ClientLocation.Contains(filter.Location));
            }

            if (!string.IsNullOrEmpty(filter.Accommodation))
            {
                queryable = queryable.Where(m => m.Accomodation.Contains(filter.Accommodation));
            }

            if (!string.IsNullOrEmpty(filter.MeanOfTransportation))
            {
                queryable = queryable.Where(m => m.MeansOfTransportation.Contains(filter.MeanOfTransportation));
            }

            if (filter.StartingDate.HasValue)
            {
                queryable = queryable.Where(m => m.StartingDate == filter.StartingDate);
            }

            if (filter.EndingDate.HasValue)
            {
                queryable = queryable.Where(m => m.EndingDate == filter.EndingDate);
            }

            return queryable.ToList().Select(b => b.ToModel());
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
