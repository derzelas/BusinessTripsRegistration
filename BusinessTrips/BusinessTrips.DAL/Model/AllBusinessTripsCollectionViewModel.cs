using System.Collections.Generic;

namespace BusinessTrips.DAL.Model
{
    public class AllBusinessTripsCollectionViewModel
    {
        public BusinessTripFilter BusinessTripFilter { get; set; }
        public IEnumerable<BusinessTripViewModel> BusinessTrips { get; set; }

        public AllBusinessTripsCollectionViewModel()
        {
            BusinessTrips = new List<BusinessTripViewModel>();
        }
    }
}
