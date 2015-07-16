using System.Collections.Generic;

namespace BusinessTrips.DAL.Model
{
    public class OtherBusinessTripsCollectionViewModel
    {
        public BusinessTripFilter BusinessTripFilter { get; set; }
        public IEnumerable<SearchBusinessTripModel> SearchBusinessTripModels { get; set; }

        public OtherBusinessTripsCollectionViewModel()
        {
            SearchBusinessTripModels = new List<SearchBusinessTripModel>();
        }
    }
}
