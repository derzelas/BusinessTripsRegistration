using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Attribute;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Model.User
{
    public class UserRegistrationModel
    {
        private const int MinimumNameLength = 3;
        private const int MinimumPasswordLength = 6;

        private readonly IRandomSaltGenerator randomSaltGenerator;
        private readonly IUserRepository userRepository;

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
        [MinLength(MinimumPasswordLength, ErrorMessage = "Minimum password length is 6")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match confirmation password.")]
        [Display(Name = "Confirm password")]
        public string ConfirmedPassword { get; set; }

        public UserRegistrationModel()
        {
            userRepository = new UserRepository();
            randomSaltGenerator = new RandomSaltGenerator();
        }

        public UserRegistrationModel(IRandomSaltGenerator randomSaltGenerator, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.randomSaltGenerator = randomSaltGenerator;
        }

        public void Save()
        {
            Id = Guid.NewGuid();

            UserEntity userEntity = ToUserEntity();

            userEntity.Roles.Add(new RoleRepository().GetRole(Role.Regular));
            userEntity.Salt = randomSaltGenerator.GetSalt();
            userEntity.HashedPassword = PasswordHasher.GetHashed(Password + userEntity.Salt);

            userRepository.Add(userEntity);
            userRepository.SaveChanges();
        }

        private UserEntity ToUserEntity()
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