using System;

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

        public SearchBusinessTripModel(BusinessTripModel businessTripModel)
        {
            Name = businessTripModel.User.Name;
            StartingDate = businessTripModel.StartingDate;
            Location = businessTripModel.ClientLocation;
            Accomodation = businessTripModel.Accomodation;
            MeansOfTransportaion = businessTripModel.MeansOfTransportation;
        }
    }
}
