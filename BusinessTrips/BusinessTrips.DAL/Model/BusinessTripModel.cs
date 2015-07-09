using System;

namespace BusinessTrips.DAL.Model
{
    public class BusinessTripModel
    {
        public Guid Id { get; set; }
        public UserModel User { get; set; }

        public string PmName { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public string TaskNumber { get; set; }
        public DepartmentType Department { get; set; }

        public string LeavingFrom { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }

        public string ClientName { get; set; }
        public string ClientLocation { get; set; }

        public bool WithPersonalCar { get; set; }
        public string MeanOfTransportation { get; set; }

        public string Accomodation { get; set; }
        public bool PhoneIsNeeded { get; set; }
        public bool BankCardIsNeeded { get; set; }

        public string OtherInfo { get; set; }

        public string Status { get; set; }

        public enum DepartmentType
        {
            Department1 = 0,
            Department2,
            Department3
        }
    }
}

