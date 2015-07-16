using System;
using System.Collections.Generic;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
{
    public class BusinessTripCollectionModel
    {
        public void LoadBusinessTripForUser(Guid userId)
        {
            var businessTripsRepository = new BusinessTripsRepository();
            //BusinessTripModels = businessTripsRepository.GetByUser(userId);
        }

        public IEnumerable<SearchBusinessTripModel> LoadOtherBusinessTrips(BusinessTripFilter filter)
        {
            var businessTripsRepository = new BusinessTripsRepository();
            return businessTripsRepository.GetOtherBusinessTrips(filter);
        }
    }
}
