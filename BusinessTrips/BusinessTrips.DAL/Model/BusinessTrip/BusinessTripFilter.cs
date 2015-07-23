﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Model.BusinessTrip;

namespace BusinessTrips.DAL.Model.BusinessTrip
{
    public class BusinessTripFilter
    {

        [DisplayName("Status")]
        public BusinessTripStatus? Status { get; set; }

        public Guid? Guid { get; set; }

        [DisplayName("Staring Date")]
        [DataType(DataType.Date)]
        public DateTime? StartingDate { get; set; }

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
