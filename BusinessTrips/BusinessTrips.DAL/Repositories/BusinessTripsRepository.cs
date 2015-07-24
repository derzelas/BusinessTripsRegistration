using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Exceptions;
using BusinessTrips.DAL.Models.BusinessTrip;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Repositories
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

        public IEnumerable<BusinessTripEntity> GetBusinessTripsBy(BusinessTripFilter filter, string[] userRole)
        {
            IQueryable<BusinessTripEntity> businessTrips = storage.GetStorageFor<BusinessTripEntity>();

            if (!string.IsNullOrEmpty(filter.UserId))
            {
                Guid filterGuid = GetGuidBy(filter.UserId);

                return businessTrips.Where(m => m.Id == filterGuid);
            }

            if (userRole.Contains(Role.Hr.ToString()))
            {
                if (filter.Status.HasValue)
                {
                    businessTrips = businessTrips.Where(m => m.Status == filter.Status);
                }
                else
                {
                    businessTrips = businessTrips.Where(AllStatusFilter());
                }
            }
            else
            {
                businessTrips = businessTrips.Where(RegularUserStatusFilter());
            }

            if (filter.StartingDate.HasValue)
            {
                businessTrips = businessTrips.Where(m => m.StartingDate >= filter.StartingDate);
            }

            if (filter.EndingDate.HasValue)
            {
                businessTrips = businessTrips.Where(m => m.EndingDate <= filter.EndingDate);
            }

            if (!string.IsNullOrEmpty(filter.Location))
            {
                businessTrips = businessTrips.Where(m => m.ClientLocation.Contains(filter.Location));
            }

            if (!string.IsNullOrEmpty(filter.Person))
            {
                businessTrips = businessTrips.Where(m => m.User.Name.Contains(filter.Person));
            }

            if (!string.IsNullOrEmpty(filter.MeansOfTransportation))
            {
                businessTrips = businessTrips.Where(m => m.MeansOfTransportation.Contains(filter.MeansOfTransportation));
            }

            if (!string.IsNullOrEmpty(filter.Accommodation))
            {
                businessTrips = businessTrips.Where(m => m.Accomodation.Contains(filter.Accommodation));
            }

            return businessTrips;
        }

        public void UpdateStatus(Guid id, BusinessTripStatus status)
        {
            BusinessTripEntity businessTripEntity = storage.GetStorageFor<BusinessTripEntity>().Single(u => u.Id == id);
            businessTripEntity.Status = status;
        }

        public void SaveChanges()
        {
            storage.SaveChanges();
        }

        private Expression<Func<BusinessTripEntity, bool>> AllStatusFilter()
        {
            return m => m.Status == BusinessTripStatus.Accepted || m.Status == BusinessTripStatus.Pending || m.Status == BusinessTripStatus.Canceled || m.Status == BusinessTripStatus.Rejected;
        }

        private Expression<Func<BusinessTripEntity, bool>> RegularUserStatusFilter()
        {
            return m => m.Status == BusinessTripStatus.Accepted || m.Status == BusinessTripStatus.Pending;
        }

        private static Guid GetGuidBy(string businessTripId)
        {
            Guid parsedId;
            if (!Guid.TryParse(businessTripId, out parsedId))
            {
                throw new InvalidIdException();
            }

            return parsedId;
        }
    }
}