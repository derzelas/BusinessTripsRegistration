using System.Collections.Generic;

namespace BusinessTrips.DAL.Model
{
    public class AllBusinessTripsCollectionViewModel
    {
        public BusinessTripFilter BusinessTripFilter { get; set; }
        public IEnumerable<SearchBusinessTripModel> SearchBusinessTripModels { get; set; }

        public AllBusinessTripsCollectionViewModel()
        {
            SearchBusinessTripModels = new List<SearchBusinessTripModel>();
        }
    }
}
