using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
{
    public class BusinessTripCollectionModel
    {
        public void LoadBusinessTripForUser(Guid userId)
        {
            BusinessTripsRepository businessTripsRepository = new BusinessTripsRepository();
            //BusinessTripModels = businessTripsRepository.GetByUser(userId);
        }

        public IEnumerable<SearchBusinessTripModel> LoadOtherBusinessTrips(BusinessTripFilter filter)
        {
            BusinessTripsRepository businessTripsRepository = new BusinessTripsRepository();
            return businessTripsRepository.GetOtherBusinessTrips(filter);
        }
    }
}
