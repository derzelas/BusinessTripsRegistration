using System;
using System.ComponentModel.DataAnnotations;
using BusinessTrips.DAL.Attribute;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Model.User
{
    public class UserRegistrationModel : PasswordRegistrationModel
    {
        private const int MinimumNameLength = 3;

        private readonly IRandomSaltGenerator randomSaltGenerator;
        private readonly IUserRepository userRepository;

        [Required]
        [Display(Name = "Name")]
        [MinLength(MinimumNameLength, ErrorMessage = "Name length must be at least 3 characters long")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        [UniqueEmail(ErrorMessage = "This e-mail is already registered")]
        public string Email { get; set; }

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

            userRepository.CreateByUserEntity(userEntity);
            userRepository.CommitChanges();
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