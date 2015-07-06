using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.Models
{
    public class UserModel
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
