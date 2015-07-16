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
    }
}
