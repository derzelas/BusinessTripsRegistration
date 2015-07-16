using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.DAL.Model
{
    public class BusinessTripFilter
    {
        public string EmployeeName { get; set; }
        public string ClientName { get; set; }
        public string Location { get; set; }
        public string MeanOfTransportation { get; set; }
        public string Accommodation { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartingDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndingDate { get; set; }
    }
}
