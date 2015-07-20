using System.Collections.Generic;

namespace BusinessTrips.DAL.Model
{
    public class PersonalBusinesTripsCollectionViewModel
    {
        public IEnumerable<PersonalBusinesTripsViewModel> PersonalBusinessTripsViewModels { get; set; }

        public PersonalBusinesTripsCollectionViewModel()
        {
            PersonalBusinessTripsViewModels = new List<PersonalBusinesTripsViewModel>();
        }
    }
}
