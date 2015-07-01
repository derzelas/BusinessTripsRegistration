using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.Models
{
    public class UserModel
    {
        public string Name { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


        public override bool Equals(object o)
        {
            if (ReferenceEquals(null, o)) return false;
            if (ReferenceEquals(this, o)) return true;
            if (o.GetType() != this.GetType()) return false;
            return Equals((UserModel) o);
        }

        protected bool Equals(UserModel other)
        {
            return string.Equals(Email, other.Email);
        }

        public override int GetHashCode()
        {
            return (Email != null ? Email.GetHashCode() : 0);
        }
    }
}