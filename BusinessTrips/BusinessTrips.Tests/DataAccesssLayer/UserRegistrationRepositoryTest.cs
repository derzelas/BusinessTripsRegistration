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

        [TestMethod]
        public void GetByTokenExistentUserRegistrationReturnsTheSameUserRegistration()
        {
            var userRegistration = new UserRegistrationModel
            {
                Name = "testName",
                Email = "123",
            };

            userRegistration.Save();

            repository.Add(userRegistration);

            var userReg = repository.GetByToken(userRegistration.RegisterToken);
            
            Assert.AreEqual(userRegistration,userReg);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetByTokenInexistentUserRegistrationThrowsException()
        {
            var userRegistration = new UserRegistrationModel
            {
                Name = "testName",
                Email = "123"
            };

            userRegistration.Save();

            Guid guid = userRegistration.RegisterToken;

            repository.Add(userRegistration);

            userRegistration.Save();

            repository.GetByToken(guid);
        }
    }
}