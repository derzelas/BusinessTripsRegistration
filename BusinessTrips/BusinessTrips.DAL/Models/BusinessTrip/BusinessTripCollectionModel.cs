using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Repositories;
using BusinessTrips.DAL.Storage;
using BusinessTrips.DAL.ViewModel;

namespace BusinessTrips.DAL.Models.BusinessTrip
{
    public class BusinessTripCollectionModel
    {
        public IEnumerable<BusinessTripViewModel> GetBusinessTripsBy(BusinessTripFilter filter, string[] userRole)
        {
            var businessTripsRepository = new BusinessTripsRepository();

            return businessTripsRepository.GetBusinessTripsBy(filter, userRole).Select(m => new BusinessTripViewModel(m));
        }
    }
}
