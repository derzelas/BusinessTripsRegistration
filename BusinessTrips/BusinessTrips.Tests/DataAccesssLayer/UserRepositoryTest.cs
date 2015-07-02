using BusinessTrips.DataAccessLayer;
using BusinessTrips.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class UserRepositoryTest
    {
        private IStorage<UserModel> storage;
        private UserRepository userRepository;
        private UserModel userModel;

        [TestInitialize]
        public void Initialize()
        {
            userRepository = new UserRepository();
            userModel = new UserModel();
        }

        [TestMethod]
        public void InstanceIsCreated()
        {
            Assert.IsNotNull(userRepository);
        }

        [TestMethod]
        public void ValidationPassesWhenCredentialsAreValid()
        {
            UserRegistrationModel userRegistrationModel = new UserRegistrationModel()
            {
                Email = "example@work.com",
                Password = "12345"
            };
           
            userRepository.CreateByUserRegistration(userRegistrationModel);

            Assert.AreEqual(true, userRepository.AreCredentialsValid(userRegistrationModel.Email, userRegistrationModel.Password));
        }

        [TestMethod]
        public void ValidationFailsWhenCredentialisAreInvalid()
        {
            userModel.Email = "notexist@work.com";
            userModel.Password = "12345";
            
            Assert.AreEqual(false, userRepository.AreCredentialsValid(userModel.Email, userModel.Password));            
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

            userModel = userRepository.CreateByUserRegistration(userRegistrationModel);

            Assert.AreEqual(userModel.Name, userRegistrationModel.Name);
            Assert.AreEqual(userModel.Email, userRegistrationModel.Email);
            Assert.AreEqual(userModel.Password, userRegistrationModel.Password);
        }
    }
}
