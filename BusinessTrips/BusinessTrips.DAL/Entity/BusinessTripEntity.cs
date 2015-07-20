using System;

namespace BusinessTrips.DAL.Entity
{
    public class BusinessTripEntity
    {
        public Guid Id { get; set; }
        public virtual UserEntity User { get; set; }

        public string PmName { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public string TaskNumber { get; set; }
        public string Department { get; set; }

        public string LeavingFrom { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }

        public string ClientName { get; set; }
        public string ClientLocation { get; set; }
        
        public string MeansOfTransportation { get; set; }

        public string Accomodation { get; set; }
        public bool PhoneIsNeeded { get; set; }
        public bool BankCardIsNeeded { get; set; }

        public string OtherInfo { get; set; }

        public BusinessTripStatus Status { get; set; }
    }
}
