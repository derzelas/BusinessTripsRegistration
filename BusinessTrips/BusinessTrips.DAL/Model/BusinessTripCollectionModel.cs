using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
{
    public class BusinessTripCollectionModel
    {
        public IEnumerable<BusinessTripModel> BusinessTripModels { get; set; }

        public void LoadBusinessTripForUser(Guid userId)
        {
            var businessTripsRepository = new BusinessTripsRepository();
            BusinessTripModels = businessTripsRepository.GetByUser(userId).Select(e=>new BusinessTripModel(e));
        }

        public IEnumerable<SearchBusinessTripModel> LoadOtherBusinessTrips(BusinessTripFilter filter)
        {
            var businessTripsRepository = new BusinessTripsRepository();

            return businessTripsRepository.GetBusinessTrips(filter).Select(m => new SearchBusinessTripModel(m));
        }
    }
}
