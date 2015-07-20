using System;
using BusinessTrips.DAL.Entity;

namespace BusinessTrips.DAL.Model
{
    public class SearchBusinessTripModel
    {
        public DateTime StartingDate { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string MeansOfTransportaion { get; set; }
        public string Accomodation { get; set; }

        public SearchBusinessTripModel() { }

        public SearchBusinessTripModel(BusinessTripEntity businessTripEntity)
        {
            Name = businessTripEntity.User.Name;
            StartingDate = businessTripEntity.StartingDate;
            Location = businessTripEntity.ClientLocation;
            Accomodation = businessTripEntity.Accomodation;
            MeansOfTransportaion = businessTripEntity.MeansOfTransportation;
        }
    }
}
