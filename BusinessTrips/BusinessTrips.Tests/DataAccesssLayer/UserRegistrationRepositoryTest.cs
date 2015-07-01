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
        private List<UserRegistrationModel> elements;
        private IStorage<UserRegistrationModel> storage;

        [TestInitialize]
        public void Initialize()
        {
            elements = new List<UserRegistrationModel>();
            storage = new InMemoryStorage<UserRegistrationModel>(elements);

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
            UserRegistrationModel userRegistration = new UserRegistrationModel();
            userRegistration.Name = "testName";
            userRegistration.Email = "123";

            repository.Add(userRegistration);
            CollectionAssert.Contains(elements,userRegistration);
        }
    }
}
