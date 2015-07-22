using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Attribute;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Model.User;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model.BusinessTrip
{
    public class BusinessTripModel
    {
        public Guid Id { get; set; }
        public UserModel User { get; set; }

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
        [DateRange]
        public DateTime StartingDate { get; set; }

        [Required]
        [Display(Name = "Ending date:")]
        [DataType(DataType.Date)]
        [DateRange]
        public DateTime EndingDate { get; set; }

        [Required]
        [Display(Name = "Client name:")]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Client location:")]
        public string ClientLocation { get; set; }

        [Display(Name = "Means of transportation:")]
        public string MeansOfTransportation { get; set; }

        [Display(Name = "Accomodation:")]
        public string Accomodation { get; set; }

        [Display(Name = "Phone is needed")]
        public bool PhoneIsNeeded { get; set; }

        [Display(Name = "Bank card is needed")]
        public bool BankCardIsNeeded { get; set; }

        [Display(Name = "Anything else you consider important:")]
        public string OtherInfo { get; set; }

        [Display(Name = "Status:")]
        public BusinessTripStatus Status { get; set; }

        private readonly BusinessTripsRepository businessTripRepository = new BusinessTripsRepository();

        public BusinessTripModel()
        {
            Id = Guid.NewGuid();
            Status = BusinessTripStatus.Pending;
        }

        public BusinessTripModel(BusinessTripEntity businessTripEntity)
        {
            Load(businessTripEntity);
        }

        public BusinessTripModel(Guid id)
        {
            var businessTripEntity = businessTripRepository.GetById(id);

            Load(businessTripEntity);
        }

        private void Load(BusinessTripEntity businessTripEntity)
        {
            Id = businessTripEntity.Id;
            User = new UserModel(businessTripEntity.User);
            PmName = businessTripEntity.PmName;
            ProjectNumber = businessTripEntity.ProjectNumber;
            ProjectName = businessTripEntity.ProjectName;
            Accomodation = businessTripEntity.Accomodation;
            BankCardIsNeeded = businessTripEntity.BankCardIsNeeded;
            ClientLocation = businessTripEntity.ClientLocation;
            ClientName = businessTripEntity.ClientName;
            Department = businessTripEntity.Department;
            EndingDate = businessTripEntity.EndingDate;
            LeavingFrom = businessTripEntity.LeavingFrom;
            MeansOfTransportation = businessTripEntity.MeansOfTransportation;
            OtherInfo = businessTripEntity.OtherInfo;
            PhoneIsNeeded = businessTripEntity.PhoneIsNeeded;
            StartingDate = businessTripEntity.StartingDate;
            Status = businessTripEntity.Status;
            TaskName = businessTripEntity.TaskName;
            TaskNumber = businessTripEntity.TaskNumber;
        }

        public void Save()
        {
            businessTripRepository.Add(this);
            businessTripRepository.CommitChanges();
        }

        public void ChangeStatus(BusinessTripStatus status)
        {
            if (Status == BusinessTripStatus.Pending || Status == BusinessTripStatus.Accepted)
            {
                Status = status;

                businessTripRepository.UpdateStatus(Id, status);
                businessTripRepository.CommitChanges();
            }
        }

        public BusinessTripEntity ToEntity()
        {
            return new BusinessTripEntity
            {
                Id = Id,
                User = User.ToEntity(),
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
            };
        }
    }
}