using System.Collections.Generic;

namespace BusinessTrips.DAL.Model
{
    public class UserBusinessTripsCollectionViewModel
    {
        public IEnumerable<UserBusinesTripsViewModel> UserBusinesTripsViewModel { get; set; }

        public UserBusinessTripsCollectionViewModel()
        {
            UserBusinesTripsViewModel = new List<UserBusinesTripsViewModel>();
        }

        public UserBusinessTripsCollectionViewModel(IEnumerable<UserBusinesTripsViewModel> enumerable)
        {
            UserBusinesTripsViewModel = enumerable;
        }
    }
}
