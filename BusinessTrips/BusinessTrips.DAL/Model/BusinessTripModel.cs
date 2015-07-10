﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.DAL.Model
{
    public class BusinessTripModel
    {
        public Guid Id { get; set; }
        public UserModel User { get; set; }

        [Required]
        [Display(Name = "PM name:")]
        public string PmName { get; set; }

        [Required]
        [Display(Name = "Project number:")]
        public string ProjectNumber { get; set; }

        [Display(Name = "Project name:")]
        public string ProjectName { get; set; }

        [Display(Name = "Task name:")]
        public string TaskName { get; set; }

        [Required]
        [Display(Name = "Task number:")]
        public string TaskNumber { get; set; }

        [Required]
        [Display(Name = "Department:")]
        public DepartmentType Department { get; set; }

        [Required]
        [Display(Name = "Leaving from:")]
        public string LeavingFrom { get; set; }

        [Required]
        [Display(Name = "Starting date:")]
        public DateTime StartingDate { get; set; }

        [Required]
        [Display(Name = "Ending date:")]
        public DateTime EndingDate { get; set; }

        [Required]
        [Display(Name = "Client name:")]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Client location:")]
        public string ClientLocation { get; set; }

        public bool WithPersonalCar { get; set; }
        public string MeanOfTransportation { get; set; }

        [Display(Name = "Accomodation:")]
        public string Accomodation { get; set; }

        [Display(Name = "A phone is needed:")]
        public bool PhoneIsNeeded { get; set; }

        [Display(Name = "Bank card is needed:")]
        public bool BankCardIsNeeded { get; set; }

        [Display(Name = "Anything else to consider important:")]
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