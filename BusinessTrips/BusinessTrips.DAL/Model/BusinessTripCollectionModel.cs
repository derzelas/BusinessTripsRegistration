using System;
using System.Collections.Generic;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
{
    public class BusinessTripCollectionModel
    {
        public BusinessTripFilter Filter { get; set; }

        public IEnumerable<BusinessTripModel> BusinessTripModels { get; private set; }

        public BusinessTripCollectionModel()
        {
            BusinessTripModels = new List<BusinessTripModel>();
        }

        public void LoadBusinessTripForUser(Guid userId)
        {
            BusinessTripsRepository businessTripsRepository = new BusinessTripsRepository();
            BusinessTripModels = businessTripsRepository.GetByUser(userId);
        }

        public void LoadOtherBusinessTrips()
        {
            BusinessTripsRepository businessTripsRepository = new BusinessTripsRepository();
            BusinessTripModels = businessTripsRepository.GetOtherBusinessTrips(Filter);
        }
    }
}
