using System;

namespace BusinessTrips.DAL.Model
{
    public class MyBusinesTripsViewModel
    {
        public Guid Id { get; set; }
        public string Department { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Accomodation { get; set; }
        public string Status { get; set; }

        public MyBusinesTripsViewModel() { }

        public MyBusinesTripsViewModel(BusinessTripModel businessTripModel)
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
