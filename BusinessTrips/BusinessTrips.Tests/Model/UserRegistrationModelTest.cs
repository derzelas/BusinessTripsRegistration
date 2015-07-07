using BusinessTrips.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.Model
{
    [TestClass]
    public class UserRegistrationModelTest
    {
        private UserRegistrationModel userRegistrationModel;

        [TestInitialize]
        public void Initialize()
        {
            userRegistrationModel=new UserRegistrationModel();
        }

        [TestMethod]
        public void SaveCreatesANewGuid()
        {
            userRegistrationModel.Save();
            Assert.IsNotNull(userRegistrationModel.Id);
        }
    }
}
