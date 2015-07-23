using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.ViewModel;

namespace BusinessTrips.DAL.Model.BusinessTrip
{
    public class BusinessTripCollectionModel
    {
        public IEnumerable<BusinessTripViewModel> GetBusinessTripsBy(BusinessTripFilter filter)
        {
            var businessTripsRepository = new BusinessTripsRepository();

            return businessTripsRepository.GetBusinessTripsBy(filter).Select(m => new BusinessTripViewModel(m));
        }
    }
}
