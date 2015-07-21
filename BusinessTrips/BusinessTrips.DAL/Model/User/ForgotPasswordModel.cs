using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.DAL.Model.User
{
    public class ForgotPasswordModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
