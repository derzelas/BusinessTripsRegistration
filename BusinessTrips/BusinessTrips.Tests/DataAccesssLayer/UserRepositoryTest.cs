using BusinessTrips.DataAccessLayer;
using BusinessTrips.Models;
using BusinessTrips.Services;
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
            UserRegistrationModel userRegistrationModel = new UserRegistrationModel()
            {
                Email = "example@work.com",
                Password = "12345"
            };
            user.CreateByUserRegistration(userRegistrationModel);

            bool actual = user.AreCredentialsValid(userRegistrationModel.Email, userRegistrationModel.Password);
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
            userModel = user.CreateByUserRegistration(userRegistrationModel);

            Assert.AreEqual(userModel.Name, userRegistrationModel.Name);
            Assert.AreEqual(userModel.Email, userRegistrationModel.Email);
            Assert.AreEqual(userModel.Password, userRegistrationModel.Password);
            Assert.IsNotNull(userModel.ID);
            Assert.AreEqual(userModel.IsConfirmed, false);
        }

        [TestMethod]
        public void GetByIDFindsCreatedUser()
        {
            userModel = user.CreateByUserRegistration(userRegistrationModel);

            UserModel actualUserModel = user.GetByID(userModel.ID);

            Assert.AreEqual(userModel, actualUserModel);
        }
    }
}