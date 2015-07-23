using System;
using System.Web.Mvc;
using BusinessTrips.Controllers;
using BusinessTrips.DAL.Model.User;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.Controllers
{
    [TestClass]
    public class UserOperationsControllerTest
    {
        private UserOperationsController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            controller = new UserOperationsController();
            EfStorage storage = new EfStorage(new EfStorageDbInitializerTest());
            storage.Database.Initialize(true);
        }

        [TestMethod]
        public void Register_ReturnRegisterView_IfUserRegistrationModelIsInvalid()
        {
            controller.ModelState.AddModelError("key", "error");

            var result = controller.Register(new UserRegistrationModel()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Register", result.ViewName);
        }

        [TestMethod]
        public void ConfirmRegistration_SetIsConfirmedPropertyToTrue_IfUserGuidExistsAndIsValid()
        {
            var userRegistrationModel = new UserRegistrationModel();
            userRegistrationModel.Save();

            var result = controller.ConfirmRegistration(userRegistrationModel.Id.ToString()) as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("RegistrationConfirmationSuccessful", result.ViewName);
        }

        [TestMethod]
        public void ConfirmRegistrationReturnsErrorViewWhenGuidIsEmpty()
        {
            var result = controller.ConfirmRegistration(string.Empty) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("ErrorEncountered", result.ViewName);
        }

        [TestMethod]
        public void ConfirmRegistrationReturnsErrorViewWhenGuidHasBadFormat()
        {
            string badFormatGuid = "5746876876876";

            var result = controller.ConfirmRegistration(badFormatGuid) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("ErrorEncountered", result.ViewName);
        }

        [TestMethod]
        public void LoginReturnsUnknownUserViewWhenUserIsNotInDatabase()
        {
            var result = controller.Login(new UserModel() { Id = Guid.NewGuid(), Password = "" }) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("InvalidUser", result.ViewName);
        }
    }
}
