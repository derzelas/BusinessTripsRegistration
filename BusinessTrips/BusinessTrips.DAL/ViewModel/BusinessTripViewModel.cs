using System;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model.BusinessTrip;

namespace BusinessTrips.DAL.ViewModel
{
    public class BusinessTripViewModel
    {
        public DateTime StartingDate { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string MeansOfTransportaion { get; set; }
        public string Accomodation { get; set; }
        public BusinessTripStatus Status { get; set; }
        public Guid Id { get; set; }

        public BusinessTripViewModel(BusinessTripEntity businessTripEntity)
        {
            Name = businessTripEntity.User.Name;
            StartingDate = businessTripEntity.StartingDate;
            Location = businessTripEntity.ClientLocation;
            Accomodation = businessTripEntity.Accomodation;
            MeansOfTransportaion = businessTripEntity.MeansOfTransportation;
            Status = businessTripEntity.Status;
            Id = businessTripEntity.Id;
        }
    }
}
