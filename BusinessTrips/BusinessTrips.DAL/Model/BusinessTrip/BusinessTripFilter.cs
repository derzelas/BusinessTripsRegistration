using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.DAL.Model.BusinessTrip
{
    public class BusinessTripFilter
    {
        [DisplayName("Status")]
        public BusinessTripStatus? Status { get; set; }

        public Guid? Guid { get; set; }

        [DisplayName("Starting Date")]
        [DataType(DataType.Date)]
        public DateTime? StartingDate { get; set; }

        [DisplayName("Ending Date")]
        [DataType(DataType.Date)]
        public DateTime? EndingDate { get; set; }

        [DisplayName("Location")]
        public string Location { get; set; }

        [DisplayName("Person")]
        public string Person { get; set; }

        [DisplayName("Mean Of Transporation")]
        public string MeanOfTransportation { get; set; }

        [DisplayName("Accomodation")]
        public string Accommodation { get; set; }    
    }
}
