using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Attribute;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model.User
{
    public class UserRegistrationModel
    {
        private const int MinimumPasswordLength = 6;
        private const int MinimumNameLength = 3;
        private const string PasswordValidationMessage = "Minimum password length is 6";

        private readonly IRandomSaltGenerator randomSaltGenerator;
        private readonly IUserRepository repository;

        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MinLength(MinimumNameLength, ErrorMessage = "Name length must be at least 3 characters long")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        [UniqueEmail(ErrorMessage = "This e-mail is already registered")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(MinimumPasswordLength, ErrorMessage = PasswordValidationMessage)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match confirmation password.")]
        [Display(Name = "Confirm password")]
        public string ConfirmedPassword { get; set; }

        public UserRegistrationModel()
        {
            repository = new UserRepository();
            randomSaltGenerator = new RandomSaltGenerator();
        }

        public UserRegistrationModel(IRandomSaltGenerator randomSaltGenerator, IUserRepository userRepository)
        {
            repository = userRepository;
            this.randomSaltGenerator = randomSaltGenerator;
        }

        // ---------- if i pass a role that is not in databate, than invalid operation exception is throw
        public void Save()
        {
            Id = Guid.NewGuid();

            UserEntity userEntity = ToUserEntity();
            userEntity.Roles.Add(new RoleRepository().GetRole("Regular"));
            userEntity.Salt = randomSaltGenerator.GetSalt();
            userEntity.HashedPassword = PasswordHasher.GetHashed(Password + userEntity.Salt);

            repository.CreateByUserEntity(userEntity);
            repository.CommitChanges();
        }

        public UserEntity ToUserEntity()
        {
            return new UserEntity()
            {
                Name = Name,
                Email = Email,
                IsConfirmed = false,
                Id = Id
            };
        }
    }
}