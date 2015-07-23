using BusinessTrips.DAL.Repository;

namespace BusinessTrips.DAL.Model.User
{
    public class SetNewPasswordModel : PasswordRegistrationModel
    {
        public void SetPassword()
        {
            var userRepository = new UserRepository();
            var userEntity = userRepository.GetBy(Id);

            ConfirmedPassword = PasswordHasher.GetHashed(ConfirmedPassword + userEntity.Salt);
            userEntity.HashedPassword = ConfirmedPassword;

            userRepository.SaveChanges();
        }
    }
}