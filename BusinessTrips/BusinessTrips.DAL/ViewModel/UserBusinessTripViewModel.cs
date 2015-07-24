using System;
using BusinessTrips.DAL.Models.BusinessTrip;

namespace BusinessTrips.DAL.ViewModel
{
    public class UserBusinessTripViewModel
    {
        public Guid Id { get; set; }
        public string Area { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Accomodation { get; set; }
        public BusinessTripStatus Status { get; set; }

        public UserBusinessTripViewModel(BusinessTripModel businessTripModel)
        {
            StartingDate = businessTripModel.StartingDate;
            EndingDate = businessTripModel.EndingDate;
            Id = businessTripModel.Id;
            Area = businessTripModel.Area;
            Accomodation = businessTripModel.Accomodation;
            Status = businessTripModel.Status;
        }
    }
}
