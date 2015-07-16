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
            var businessTripEntity = storage.GetSetFor<BusinessTripEntity>().FirstOrDefault(m => m.Id == id);

            if (businessTripEntity != null)
            {
                return businessTripEntity.ToModel();
            }
            return null;
        }

        public IEnumerable<BusinessTripModel> GetByUser(Guid id)
        {
            return storage.GetSetFor<BusinessTripEntity>().Where(e => e.User.Id == id).ToList().Select(e => e.ToModel());
        }

        public IEnumerable<SearchBusinessTripModel> GetOtherBusinessTrips(BusinessTripFilter filter)
        {
            var queryable = storage.GetSetFor<BusinessTripEntity>();

            if (!string.IsNullOrEmpty(filter.EmployeeName))
            {
                queryable = queryable.Where(m => m.User.Name.Contains(filter.EmployeeName));
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

            return queryable.ToList().Select(e => e.ToSearchBusinessTripViewModel());
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
