using System;
using BusinessTrips.Models;

namespace BusinessTrips.DataAccessLayer
{
    public class UserRegistrationEntity
    {
        public bool IsConfirmed { get; set; }
        public UserModel User { get; set; }
        public Guid Id { get; set; }
    }
}