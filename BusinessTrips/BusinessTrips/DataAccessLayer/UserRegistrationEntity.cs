using System;
using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRegistrationEntity
    {
        public bool IsConfirmed { get; set; }
        public UserModels User { get; set; }
        public Guid Id { get; set; }
    }
}