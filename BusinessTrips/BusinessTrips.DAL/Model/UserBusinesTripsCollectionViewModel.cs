using System.Collections.Generic;

namespace BusinessTrips.DAL.Model
{
    public class UserBusinesTripsCollectionViewModel
    {
        public IEnumerable<UserBusinesTripsViewModel> UserBusinessTripsViewModels { get; set; }

        public UserBusinesTripsCollectionViewModel()
        {
            UserBusinessTripsViewModels = new List<UserBusinesTripsViewModel>();
        }
    }
}
