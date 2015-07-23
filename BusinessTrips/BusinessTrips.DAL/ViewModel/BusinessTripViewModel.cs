using System;
using BusinessTrips.DAL.Entity;

namespace BusinessTrips.DAL.ViewModel
{
    public class BusinessTripViewModel
    {
        public DateTime StartingDate { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string MeansOfTransportaion { get; set; }
        public string Accomodation { get; set; }

        public BusinessTripViewModel(BusinessTripEntity businessTripEntity)
        {
            StartingDate = businessTripEntity.StartingDate;
            Location = businessTripEntity.ClientLocation;
            Name = businessTripEntity.User.Name;
            MeansOfTransportaion = businessTripEntity.MeansOfTransportation;
            Accomodation = businessTripEntity.Accomodation;
        }
    }
}
