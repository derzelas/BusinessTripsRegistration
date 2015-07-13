using System;
using System.Data.Entity;
using System.Web.Mvc;
using BusinessTrips.Controllers;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.Controllers
{
    [TestClass]
    public class UserOperationsControllerTest
    {
        private UserOperationsController controller;

        [TestInitialize]
        public void Initialize()
        {
            controller = new UserOperationsController();
            EfStorage storage = new EfStorage(new DropCreateDatabaseAlways<EfStorage>());
            storage.Database.Initialize(true);
        }

        [TestMethod]
        public void RegisterReturnRegisterViewIfUserRegistrationModelIsInvalid()
        {
            controller.ModelState.AddModelError("key", "error");

            var result = controller.Register(new UserRegistrationModel()) as ViewResult;

            Assert.AreEqual("Register", result.ViewName);
        }

        [TestMethod]
        public void ConfirmRegistrationSetIsConfirmedPropertyToTrueIfUserGuidExistsAndIsValid()
        {
            var userRegistrationModel = new UserRegistrationModel();

            userRegistrationModel.Save();

            var repository = new UserRepository();


            var registrationConfirmationModel = new RegistrationConfirmationModel();
            registrationConfirmationModel.Id = userRegistrationModel.Id;
            registrationConfirmationModel.Confirm();

            UserModel userModel = repository.GetById(registrationConfirmationModel.Id);

            Assert.AreEqual(userModel.IsConfirmed, true);
        }

        [TestMethod]
        public void ConfirmRegistrationReturnsConfirmViewWhenGuidIsValid()
        {
            var result = controller.ConfirmRegistration(Guid.NewGuid().ToString()) as ViewResult;

            Assert.AreEqual("ConfirmRegistration", result.ViewName);
        }

        [TestMethod]
        public void ConfirmRegistrationReturnsErrorViewWhenGuidIsEmpty()
        {
            var registrationConfirmationModel = new RegistrationConfirmationModel();

            registrationConfirmationModel.Confirm();

            var result = controller.ConfirmRegistration(string.Empty) as ViewResult;

            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void ConfirmRegistrationReturnsErrorViewWhenGuidHasBadFormat()
        {
            var registrationConfirmationModel = new RegistrationConfirmationModel();

            registrationConfirmationModel.Confirm();

            string badFormatGuid = "5746876876876";

            var result = controller.ConfirmRegistration(badFormatGuid) as ViewResult;

            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void LoginReturnsUnknownUserViewWhenUserIsNotInDatabase()
        {
            var result = controller.Login(new UserModel() { Id = Guid.NewGuid(), Password = "" }) as ViewResult;

            Assert.AreEqual("UnknownUser", result.ViewName);
        }

        [TestMethod]
        public void LoginReturnsAuthenticatedUserViewWhenUserIsInDatabase()
        {
            var userRegistrationModel = new UserRegistrationModel()
            {
                Id = Guid.NewGuid(),
                Email = "example@gmail.com",
                Name = "name",
                Password = "password",
                ConfirmedPassword = "password"
            };
            userRegistrationModel.Save();

            var repository = new UserRepository();

            repository.Confirm(userRegistrationModel.Id);
            repository.CommitChanges();

            var userModel = repository.GetById(userRegistrationModel.Id);
            userModel.Password = "password";

            var result = controller.Login(userModel) as ViewResult;

            Assert.AreEqual("AuthenticatedUser", result.ViewName);
        }
    }
}
