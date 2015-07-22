using System.Collections.Generic;
using BusinessTrips.DAL.Model.User;

namespace BusinessTrips.DAL.ViewModel
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
