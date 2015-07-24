using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Models.BusinessTrip;

namespace BusinessTrips.DAL.ViewModel
{
    public class BusinessTripViewModel
    {
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartingDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndingDate { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string MeansOfTransportaion { get; set; }
        public string Accomodation { get; set; }
        public BusinessTripStatus Status { get; set; }
        public Guid Id { get; set; }

        public BusinessTripViewModel(BusinessTripEntity businessTripEntity)
        {
            StartingDate = businessTripEntity.StartingDate;
            EndingDate = businessTripEntity.EndingDate;
            Location = businessTripEntity.ClientLocation;
            Name = businessTripEntity.User.Name;
            MeansOfTransportaion = businessTripEntity.MeansOfTransportation;
            Accomodation = businessTripEntity.Accomodation;
            Status = businessTripEntity.Status;
            Id = businessTripEntity.Id;
        }
    }
}
