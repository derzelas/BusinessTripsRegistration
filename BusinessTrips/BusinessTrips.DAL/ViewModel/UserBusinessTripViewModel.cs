using System;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Model.BusinessTrip;

namespace BusinessTrips.DAL.ViewModel
{
    public class UserBusinessTripViewModel
    {
        public Guid Id { get; set; }
        public string Department { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Accomodation { get; set; }
        public BusinessTripStatus Status { get; set; }

        public UserBusinessTripViewModel() { }

        public UserBusinessTripViewModel(BusinessTripModel businessTripModel)
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
