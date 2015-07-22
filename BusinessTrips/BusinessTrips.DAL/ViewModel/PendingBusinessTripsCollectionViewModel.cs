using System.Collections.Generic;

namespace BusinessTrips.DAL.ViewModel
{
    public class PendingBusinessTripsCollectionViewModel
    {        
        public IEnumerable<PendingBusinessTripViewModel> BusinessTrips { get; set; }

        public PendingBusinessTripsCollectionViewModel()
        {
            BusinessTrips = new List<PendingBusinessTripViewModel>();
        }
    }
}