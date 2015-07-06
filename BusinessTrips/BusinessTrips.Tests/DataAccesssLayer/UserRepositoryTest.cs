using BusinessTrips.DataAccessLayer;
using BusinessTrips.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class UserRepositoryTest
    {
        private UserRepository user;
        private UserModel userModel;

        [TestInitialize]
        public void Initialize()
        {
            user = new UserRepository();
            userModel = new UserModel();
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
            UserRegistrationModel userRegistrationModel = new UserRegistrationModel()
            {
                Name = "Foo",
                Email = "example@test.com",
                Password = "12345"
            };

            userModel = user.CreateByUserRegistration(userRegistrationModel);

            Assert.AreEqual(userModel.Name, userRegistrationModel.Name);
            Assert.AreEqual(userModel.Email, userRegistrationModel.Email);
            Assert.AreEqual(userModel.Password, userRegistrationModel.Password);
        }
    }
}
