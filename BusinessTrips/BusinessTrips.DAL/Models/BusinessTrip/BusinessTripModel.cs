﻿using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Attributes;
using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Models.User;
using BusinessTrips.DAL.Repositories;

namespace BusinessTrips.DAL.Models.BusinessTrip
{
    public class BusinessTripModel
    {
        public Guid Id { get; set; }
        public UserModel User { get; set; }

        [Required]
        [Display(Name = "Area:")]
        public string Area { get; set; }

        [Required]
        [Display(Name = "PM name:")]
        public string PmName { get; set; }

        [Display(Name = "Project name:")]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Project number:")]
        public string ProjectNumber { get; set; }

        [Display(Name = "Task name:")]
        public string TaskName { get; set; }

        [Required]
        [Display(Name = "Task number:")]
        public string TaskNumber { get; set; }

        [Required]
        [Display(Name = "Leaving from:")]
        public string LeavingFrom { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required]
        [Display(Name = "Starting date:")]
        [DataType(DataType.Date)]
        [DateRange]       
        public DateTime StartingDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
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

        [Required]
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
            FromEntity(businessTripEntity);
        }

        public BusinessTripModel(Guid id)
        {
            var businessTripEntity = businessTripRepository.GetById(id);

            FromEntity(businessTripEntity);
        }

        private void FromEntity(BusinessTripEntity businessTripEntity)
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
            Area = businessTripEntity.Area;
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
            businessTripRepository.SaveChanges();
        }

        public void ChangeStatus(BusinessTripStatus status)
        {
            if (Status == BusinessTripStatus.Pending || Status == BusinessTripStatus.Accepted)
            {
                Status = status;

                businessTripRepository.UpdateStatus(Id, status);
                businessTripRepository.SaveChanges();
            }
        }

        public BusinessTripEntity ToEntity()
        {
            return new BusinessTripEntity
            {
                Id = Id,
                User = User.GetEntity(),
                PmName = PmName,
                ProjectNumber = ProjectNumber,
                ProjectName = ProjectName,
                Accomodation = Accomodation,
                BankCardIsNeeded = BankCardIsNeeded,
                ClientLocation = ClientLocation,
                ClientName = ClientName,
                Area = Area,
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