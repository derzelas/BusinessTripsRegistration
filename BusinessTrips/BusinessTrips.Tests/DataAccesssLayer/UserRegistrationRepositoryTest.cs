using System;
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
        private List<UserRegistrationModels> elements;
        private IStorage<UserRegistrationModels> storage;

        [TestInitialize]
        public void Initialize()
        {
            elements = new List<UserRegistrationModels>();
            storage = new InMemoryStorage<UserRegistrationModels>(elements);

            repository=new UserRegistrationRepository(storage);
        }

        [TestMethod]
        public void InstanceIsCreated()
        {
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void AddingElementIsInStorage()
        {
            UserRegistrationModels userRegistration = new UserRegistrationModels();
            userRegistration.Name = "testName";
            userRegistration.Email = "123";

            repository.Add(userRegistration);
            CollectionAssert.Contains(elements,userRegistration);
        }
    }
}
