using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Attributes;

namespace BusinessTrips.DAL.Models.BusinessTrip
{
    public class BusinessTripFilter
    {
        [DisplayName("Status")]
        public BusinessTripStatus? Status { get; set; }

        public string UserId { get; set; }

        [DisplayName("Starting Date")]
        [DataType(DataType.Date)]
        [DateRange]  
        public DateTime? StartingDate { get; set; }

        [DisplayName("Ending Date")]
        [DataType(DataType.Date)]
        [DateRange]    
        public DateTime? EndingDate { get; set; }

        [DisplayName("Location")]
        public string Location { get; set; }

        [DisplayName("Person")]
        public string Person { get; set; }

        [DisplayName("Means Of Transporation")]
        public string MeansOfTransportation { get; set; }

        [DisplayName("Accomodation")]
        public string Accommodation { get; set; }    
    }
}
