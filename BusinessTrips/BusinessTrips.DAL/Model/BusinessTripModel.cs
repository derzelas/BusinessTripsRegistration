using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model
{
    public class BusinessTripModel
    {
        public Guid Id { get; set; }
        public UserEntity User { get; set; }

        [Required]
        [Display(Name = "Department:")]
        public string Department { get; set; }

        [Required]
        [Display(Name = "PM name:")]
        public string PmName { get; set; }


        [Display(Name = "Project number:")]
        public string ProjectNumber { get; set; }

        [Display(Name = "Project name:")]
        public string ProjectName { get; set; }

        [Display(Name = "Task name:")]
        public string TaskName { get; set; }


        [Display(Name = "Task number:")]
        public string TaskNumber { get; set; }

        [Required]
        [Display(Name = "Leaving from:")]
        public string LeavingFrom { get; set; }

        [Required]
        [Display(Name = "Starting date:")]
        [DataType(DataType.Date)]
        [DateRangeAttribute]
        public DateTime StartingDate { get; set; }

        [Required]
        [Display(Name = "Ending date:")]
        [DataType(DataType.Date)]
        [DateRangeAttribute]
        public DateTime EndingDate { get; set; }

        [Required]
        [Display(Name = "Client name:")]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Client location:")]
        public string ClientLocation { get; set; }

        [Display(Name = "With personal car:")]
        public bool WithPersonalCar { get; set; }

        [Display(Name = "Means of transportation:")]
        public string MeansOfTransportation { get; set; }

        [Display(Name = "Accomodation:")]
        public string Accomodation { get; set; }

        [Display(Name = "A phone is needed:")]
        public bool PhoneIsNeeded { get; set; }

        [Display(Name = "Bank card is needed:")]
        public bool BankCardIsNeeded { get; set; }

        [Display(Name = "Anything else you consider important:")]
        public string OtherInfo { get; set; }

        [Display(Name = "Status:")]
        public string Status { get; set; }

        public void Save()
        {
            Id = Guid.NewGuid();
            Status = "Pending";

            var businessTripRepository = new BusinessTripsRepository();
            businessTripRepository.Add(this);
            businessTripRepository.CommitChanges();
        }
        
        public void ChangeStatus(string status)
        {
            BusinessTripsRepository businessTripsRepository = new BusinessTripsRepository();
            BusinessTripModel businessTripModel = businessTripsRepository.GetById(Id);

            if (businessTripModel != null && businessTripModel.Status == "Pending")
            {
                businessTripsRepository.UpdateStatus(businessTripModel.Id, status);
                businessTripsRepository.CommitChanges();
            }
        }
        public BusinessTripEntity ToEntity()
        {
            return new BusinessTripEntity()
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