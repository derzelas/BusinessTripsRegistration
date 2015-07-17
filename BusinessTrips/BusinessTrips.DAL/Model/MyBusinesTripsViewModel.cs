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
        public RequestStatus Status { get; set; }
    }
}
