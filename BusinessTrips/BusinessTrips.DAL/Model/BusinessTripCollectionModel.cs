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

        public void LoadBusinessTripsForUser(Guid userId)
        {
            var businessTripsRepository = new BusinessTripsRepository();
            BusinessTripModels = businessTripsRepository.GetByUser(userId);
        }

        public void LoadOtherBusinessTrips()
        {
            var businessTripsRepository = new BusinessTripsRepository();
            BusinessTripModels = businessTripsRepository.GetOtherBusinessTrips(Filter);
        }
    }
}
