using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.ViewModel;

namespace BusinessTrips.DAL.Model.BusinessTrip
{
    public class PendingBusinessTripCollectionModel
    {
        public IEnumerable<PendingBusinessTripViewModel> GetPendingBusinessTrips()
        {
            var businessTripsRepository = new BusinessTripsRepository();

            return businessTripsRepository.GetPendingBusinessTrips().Select(m => new PendingBusinessTripViewModel(m));
        }
    }
}
