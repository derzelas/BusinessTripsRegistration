using System;
using System.Web.Mvc;
using BusinessTrips.Controllers;
using BusinessTrips.DAL.Exception;
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
        public void Register_UserRegistrationModelIsInvalid_ReturnsRegisterView()
        {
            controller.ModelState.AddModelError("key", "error");

            var result = controller.Register(new UserRegistrationModel()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Register", result.ViewName);
        }

        [TestMethod]
        public void Register_UserRegistrationModelIsValid_ReturnsRegistrationSuccessfulView()
        {
            var validUserRegistrationModel = new UserRegistrationModel
            {
                Email = "example@gmail.com",
                Password = "123456",
                ConfirmedPassword = "123456"
            };

            var result = controller.Register(validUserRegistrationModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("RegistrationSuccessful", result.ViewName);
        }

        [TestMethod]
        public void ConfirmRegistration_GuidIsValidForExistentUser_ReturnRegistrationConfirmationSuccessfulView()
        {
            var userRegistrationModel = new UserRegistrationModel();
            userRegistrationModel.Save();

            var result = controller.ConfirmRegistration(userRegistrationModel.Id.ToString()) as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.AreEqual("RegistrationConfirmationSuccessful", result.ViewName);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void ConfirmRegistration_GuidIsValidAndNoUser_ThrowsUserNotFoundException()
        {
            controller.ConfirmRegistration(Guid.NewGuid().ToString());
        }

        [TestMethod]
        public void Login_UserIsNotInStorage_ReturnsUnknownUserView()
        {
            var result = controller.Login(new UserModel() { Id = Guid.NewGuid(), Password = "" }) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("InvalidUser", result.ViewName);
        }

        [TestMethod]
        public void ForgotPassword_UserIsInvalid_ReturnsEmailSentView()
        {
            var result = controller.ForgotPassword(new ForgotPasswordModel() { Email = "", Id = Guid.Empty }) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("ForgotPasswordEmailSent", result.ViewName);
            
        }
    }
}
