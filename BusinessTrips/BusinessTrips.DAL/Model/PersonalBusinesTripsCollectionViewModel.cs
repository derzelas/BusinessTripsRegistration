using System.Collections.Generic;

namespace BusinessTrips.DAL.Model
{
    public class PersonalBusinesTripsCollectionViewModel
    {
        public IEnumerable<PersonalBusinesTripsViewModel> MyBusinesTripsViewModels { get; set; }

        public PersonalBusinesTripsCollectionViewModel()
        {
            MyBusinesTripsViewModels = new List<PersonalBusinesTripsViewModel>();
        }
    }
}
