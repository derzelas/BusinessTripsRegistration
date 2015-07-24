using BusinessTrips.DAL.Exceptions;
using BusinessTrips.DAL.Models.User;
using BusinessTrips.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Models
{
    [TestClass]
    public class RegistrationConfirmationModelTests
    {
        private RegistrationConfirmationModel registrationConfirmationModel;

        [TestInitialize]
        public void TestInitialize()
        {
            registrationConfirmationModel = new RegistrationConfirmationModel();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidIdException))]
        public void Confirm_UnableToParseId_ThrowsInvalidIdException()
        {
            registrationConfirmationModel.Confirm("bad-format-guid asdads");
        }

        [TestMethod]
        public void Confirm_UserIdIsValidAndExists_SetIsConfirmedPropertyToTrue()
        {
            var userRegistrationModel = new UserRegistrationModel
            {
                Name = "name",
                Email = "example@gmail.com",
                Password = "password",
                ConfirmedPassword = "password"
            };

            userRegistrationModel.Save();

            registrationConfirmationModel.Confirm(userRegistrationModel.Id.ToString());

            var retrievedEntity = new UserRepository().GetBy(userRegistrationModel.Id);

            Assert.AreEqual(retrievedEntity.IsConfirmed, true);
        }
    }
}
