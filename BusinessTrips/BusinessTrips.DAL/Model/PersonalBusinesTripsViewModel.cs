using System;

namespace BusinessTrips.DAL.Model
{
    public class PersonalBusinesTripsViewModel
    {
        public Guid Id { get; set; }
        public string Department { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Accomodation { get; set; }
        public RequestStatus Status { get; set; }

        public PersonalBusinesTripsViewModel() { }

        public PersonalBusinesTripsViewModel(BusinessTripModel businessTripModel)
        {
            StartingDate = businessTripModel.StartingDate;
            EndingDate = businessTripModel.EndingDate;
            Id = businessTripModel.Id;
            Department = businessTripModel.Department;
            Accomodation = businessTripModel.Accomodation;
            Status = businessTripModel.Status;
        }
    }
}
