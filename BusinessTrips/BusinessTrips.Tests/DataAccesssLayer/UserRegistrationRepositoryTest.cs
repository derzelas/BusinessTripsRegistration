using System;
using BusinessTrips.DataAccessLayer;
using BusinessTrips.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class UserRegistrationRepositoryTest
    {
        private UserRegistrationRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new UserRegistrationRepository();
        }

        [TestMethod]
        public void AddedUserRegistrationIsFoundStorage()
        {
            var userRegistration = new UserRegistrationModel
            {
                Name = "testName",
                Email = "123"
            };

            repository.Add(userRegistration);
            var user = repository.Get(userRegistration);
            Assert.AreEqual(userRegistration, user);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void GetInexistentUserRegistrationThrowsException()
        {
            var userRegistration = new UserRegistrationModel
            {
                Name = "testName",
                Email = "123"
            };
            repository.Get(userRegistration);
        }
    }
}