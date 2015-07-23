using System;
using System.Web.Mvc;

namespace BusinessTrips.DAL.ViewModel
{
    public class ResetPasswordViewModel
    {
        [HiddenInput]
        public Guid  Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
