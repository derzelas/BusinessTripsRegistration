using BusinessTrips.DataAccessLayer;
using BusinessTrips.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class UserRepositoryTest
    {
        private IStorage<UserModel> storage;
        private User user;
        private UserModel userModel;

        [TestInitialize]
        public void Initialize()
        {
            user = new User();
            userModel = new UserModel();
        }

        [TestMethod]
        public void InstanceIsCreated()
        {
            Assert.IsNotNull(user);
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

            Assert.AreEqual(true, user.AreCredentialsValid(userRegistrationModel.Email, userRegistrationModel.Password));
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
