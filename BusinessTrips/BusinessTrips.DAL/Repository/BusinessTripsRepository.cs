using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Exception;
using BusinessTrips.DAL.Model.BusinessTrip;
using BusinessTrips.DAL.Model.User;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repository
{
    public class BusinessTripsRepository
    {
        private readonly IStorage storage;

        public BusinessTripsRepository()
        {
            storage = new StorageFactory().Create();
        }

        public void Add(BusinessTripModel businessTripModel)
        {
            storage.Add(businessTripModel.ToEntity());
        }

        public BusinessTripEntity GetById(Guid businessTripId)
        {
            var businessTrip = storage.GetStorageFor<BusinessTripEntity>().SingleOrDefault(m => m.Id == businessTripId);

            if (businessTrip == null)
            {
                throw new BusinessTripNotFoundException();
            }

            return businessTrip;
        }

        public IEnumerable<BusinessTripEntity> GetByUser(Guid userId)
        {
            return storage.GetStorageFor<BusinessTripEntity>().Where(e => e.User.Id == userId);
        }

        public IEnumerable<BusinessTripEntity> GetBusinessTripsBy(BusinessTripFilter filter)
        {
            IQueryable<BusinessTripEntity> businessTrips = storage.GetStorageFor<BusinessTripEntity>();

            if (!string.IsNullOrEmpty(filter.Person))
            {
                businessTrips = businessTrips.Where(m => m.User.Name.Contains(filter.Person));
            }

            if (!string.IsNullOrEmpty(filter.ClientName))
            {
                businessTrips = businessTrips.Where(m => m.ClientName.Contains(filter.ClientName));
            }

            if (!string.IsNullOrEmpty(filter.Location))
            {
                businessTrips = businessTrips.Where(m => m.ClientLocation.Contains(filter.Location));
            }

            if (!string.IsNullOrEmpty(filter.Accommodation))
            {
                businessTrips = businessTrips.Where(m => m.Accomodation.Contains(filter.Accommodation));
            }

            if (!string.IsNullOrEmpty(filter.MeanOfTransportation))
            {
                businessTrips = businessTrips.Where(m => m.MeansOfTransportation.Contains(filter.MeanOfTransportation));
            }

            if (filter.StartingDate.HasValue)
            {
                businessTrips = businessTrips.Where(m => m.StartingDate == filter.StartingDate);
            }

            if (filter.EndingDate.HasValue)
            {
                businessTrips = businessTrips.Where(m => m.EndingDate == filter.EndingDate);
            }

            return businessTrips;
        }

        public IEnumerable<BusinessTripEntity> GetPendingBusinessTrips()
        {
            IQueryable<BusinessTripEntity> businessTrips = storage.GetStorageFor<BusinessTripEntity>();
            businessTrips = businessTrips.Where(m => m.Status == BusinessTripStatus.Pending);

            return businessTrips;
        }

        public void UpdateStatus(Guid id, BusinessTripStatus status)
        {
            BusinessTripEntity businessTripEntity = storage.GetStorageFor<BusinessTripEntity>().Single(u => u.Id == id);
            businessTripEntity.Status = status;
        }

        public void CommitChanges()
        {
            storage.SaveChanges();
        }
    }
}
