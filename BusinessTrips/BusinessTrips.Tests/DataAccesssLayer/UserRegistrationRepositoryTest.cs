using System.Collections.Generic;
using BusinessTrips.DataAccessLayer;
using BusinessTrips.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class UserRegistrationRepositoryTest
    {
        private UserRegistrationRepository repository;
        private IStorage<UserRegistrationModel> storage;
        private List<UserRegistrationModel> userRegistrationModels;

        [TestInitialize]
        public void Initialize()
        {
            userRegistrationModels = new List<UserRegistrationModel>();
            storage = InMemoryStorage<UserRegistrationModel>.GetInstace(userRegistrationModels);
            repository = new UserRegistrationRepository(storage);
        }

        [TestCleanup]
        public void CleanUp()
        {
            storage = null;
        }


        [TestMethod]
        public void InstanceIsCreated()
        {
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void AddedElementIsInStorage()
        {
            var userRegistration = new UserRegistrationModel();
            userRegistration.Name = "testName";
            userRegistration.Email = "123";

            repository.Add(userRegistration);
            CollectionAssert.Contains(userRegistrationModels, userRegistration);
        }
    }
}