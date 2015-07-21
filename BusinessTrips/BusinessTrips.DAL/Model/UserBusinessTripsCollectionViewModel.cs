using System.Collections.Generic;

namespace BusinessTrips.DAL.Model
{
    public class UserBusinessTripsCollectionViewModel
    {
        public IEnumerable<UserBusinessTripViewModel> UserBusinesTrips { get; set; }

        public UserBusinessTripsCollectionViewModel()
        {
            UserBusinesTrips = new List<UserBusinessTripViewModel>();
        }

        public UserBusinessTripsCollectionViewModel(IEnumerable<UserBusinessTripViewModel> enumerable)
        {
            UserBusinesTrips = enumerable;
        }
    }
}
