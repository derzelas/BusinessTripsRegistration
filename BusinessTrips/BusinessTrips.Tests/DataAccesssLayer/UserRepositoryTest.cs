using BusinessTrips.DAL;
using BusinessTrips.DAL.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class UserRepositoryTest
    {
        private UserRepository user;
        private UserModel userModel;
        private UserRegistrationModel userRegistrationModel;

        [TestInitialize]
        public void Initialize()
        {
            user = new UserRepository();
            userModel = new UserModel();

            userRegistrationModel = new UserRegistrationModel()
            {
                Name = "Foo",
                Email = "example@test.com",
                Password = "12345",
            };

            userRegistrationModel.Save();
        }

        [TestMethod]
        public void ValidationPassesWhenCredentialsAreValid()
        {
            UserRegistrationModel registrationModel = new UserRegistrationModel()
            {
                Email = "example@work.com",
                Password = "12345"
            };
            user.CreateByUserRegistration(registrationModel);

            bool actual = user.AreCredentialsValid(registrationModel.Email, registrationModel.Password);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void ValidationFailsWhenCredentialisAreInvalid()
        {
            userModel.Email = "notexist@work.com";
            userModel.Password = "12345";

            Assert.AreEqual(false, user.AreCredentialsValid(userModel.Email, userModel.Password));
        }

        [TestMethod]
        public void CreatedUserMatchesRegistrationUser()
        {
            user.CreateByUserRegistration(userRegistrationModel);

            Assert.AreEqual(userModel.Name, userRegistrationModel.Name);
            Assert.AreEqual(userModel.Email, userRegistrationModel.Email);
            Assert.AreEqual(userModel.Password, userRegistrationModel.Password);
            Assert.IsNotNull(userModel.Id);
            Assert.AreEqual(userModel.IsConfirmed, false);
        }

        [TestMethod]
        public void GetByIdFindsCreatedUser()
        {
            user.CreateByUserRegistration(userRegistrationModel);

            UserModel actualUserModel = user.GetById(userModel.Id);

            Assert.AreEqual(userModel, actualUserModel);
        }
    }
}