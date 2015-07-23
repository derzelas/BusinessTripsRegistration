using System;
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

        public IEnumerable<PendingBusinessTripViewModel> GetPendingBusinessTripsById(Guid id)
        {
            var businessTripsRepository = new BusinessTripsRepository();

            return businessTripsRepository.GetPendingBusinessTrips().Where(entity => entity.Id == id).Select(m => new PendingBusinessTripViewModel(m));
        }
    }
}
