using System;
using System.Web.Mvc;
using BusinessTrips.Controllers;
using BusinessTrips.DAL.Model;
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
        }

        [TestMethod]
        public void RegisterReturnRegisterViewIfUserRegistrationModelIsInvalid()
        {
            controller.ModelState.AddModelError("key", "error");

            var result = controller.Register(new UserRegistrationModel()) as ViewResult;

            Assert.AreEqual("Register", result.ViewName);
        }
    }
}
