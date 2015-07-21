using System.Data.Entity;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Models
{
    [TestClass]
    public class UserModelTest
    {
        private UserRegistrationModel userRegistrationModel;

        [TestInitialize]
        public void TestMethod1()
        {
            EfStorage efStorage = new EfStorage(new DropCreateDatabaseAlways<EfStorage>());
            efStorage.Database.Initialize(true);

            userRegistrationModel = new UserRegistrationModel()
            {
                Name = "nume",
                Email = "email@email.com",
                Password = "password",
                ConfirmedPassword = "password",
            };

            userRegistrationModel.Save();
        }

        [TestMethod]
        public void Authenthicate_ValidAndConfirmedUser_ReturnTrue()
        {
            RegistrationConfirmationModel registrationConfirmationModel = new RegistrationConfirmationModel()
            {
                Id = userRegistrationModel.Id
            };

            registrationConfirmationModel.Confirm();

            UserModel userModel = new UserModel()
            {
                Email = userRegistrationModel.Email,
                Password = userRegistrationModel.Password
            };

            var actual = userModel.Authenthicate();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Authenthicate_ValidAndNotConfirmedUser_ReturnFalse()
        {
            UserModel userModel = new UserModel()
            {
                Email = userRegistrationModel.Email,
                Password = userRegistrationModel.Password
            };

            var actual = userModel.Authenthicate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Authenthicate_InexistentEmail_ReturnFalse()
        {
            UserModel userModel = new UserModel()
            {
                Email = "email@email.com",
                Password = "123"
            };

            var actual = userModel.Authenthicate();

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Authenthicate_ExistentConfirmedEmailWrongPassword_ReturnFalse()
        {
            RegistrationConfirmationModel registrationConfirmationModel = new RegistrationConfirmationModel()
            {
                Id = userRegistrationModel.Id
            };

            registrationConfirmationModel.Confirm();

            UserModel userModel = new UserModel()
            {
                Email = userRegistrationModel.Email,
                Password = "incorect"
            };

            var actual = userModel.Authenthicate();

            Assert.AreEqual(false, actual);
        }
    }
}
