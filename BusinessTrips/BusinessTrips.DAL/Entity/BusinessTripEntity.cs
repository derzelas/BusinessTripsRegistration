using System;
using BusinessTrips.DAL.Model;

namespace BusinessTrips.DAL.Entity
{
    public class BusinessTripEntity
    {
        public Guid Id { get; set; }
        public UserModel User { get; set; }

        public string PmName { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public string TaskNumber { get; set; }
        public BusinessTripModel.DepartmentType Department { get; set; }

        public BusinessTripModel.LeavingLocation LeavingFrom { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }

        public string ClientName { get; set; }
        public string ClientLocation { get; set; }

        public bool WithPersonalCar { get; set; }
        public string MeansOfTransportation { get; set; }

        public string Accomodation { get; set; }
        public bool PhoneIsNeeded { get; set; }
        public bool BankCardIsNeeded { get; set; }

        public string OtherInfo { get; set; }

        public string Status { get; set; }

        public BusinessTripModel ToModel()
        {
            return new BusinessTripModel
            {
                Id = Id,
                User = User,
                PmName = PmName,
                ProjectNumber = ProjectNumber,
                ProjectName = ProjectName,
                Accomodation = Accomodation,
                BankCardIsNeeded = BankCardIsNeeded,
                ClientLocation = ClientLocation,
                ClientName = ClientName,
                Department = Department,
                EndingDate = EndingDate,
                LeavingFrom = LeavingFrom,
                MeansOfTransportation = MeansOfTransportation,
                OtherInfo = OtherInfo,
                PhoneIsNeeded = PhoneIsNeeded,
                StartingDate = StartingDate,
                Status = Status,
                TaskName = TaskName,
                TaskNumber = TaskNumber,
                WithPersonalCar = WithPersonalCar
            };
        }
    }
}
