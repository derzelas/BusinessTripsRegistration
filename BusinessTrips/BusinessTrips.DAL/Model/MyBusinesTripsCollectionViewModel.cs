using System.Collections.Generic;

namespace BusinessTrips.DAL.Model
{
    public class MyBusinesTripsCollectionViewModel
    {
        public IEnumerable<MyBusinesTripsViewModel> MyBusinesTripsViewModels { get; set; }

        public MyBusinesTripsCollectionViewModel()
        {
            MyBusinesTripsViewModels = new List<MyBusinesTripsViewModel>();
        }
    }
}
