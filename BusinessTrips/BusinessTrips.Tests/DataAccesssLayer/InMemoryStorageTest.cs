using System;
using System.Collections.Generic;
using BusinessTrips.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class InMemoryStorageTest
    {
        private List<int> elements;
        private InMemoryStorage<int> storage;

        [TestInitialize]
        public void Initialize()
        {
            elements = new List<int>();
            storage = new InMemoryStorage<int>(elements);
        }

        [TestMethod]
        public void AnInstanceIsCreated()
        {
            Assert.IsNotNull(storage);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void GetInexistendElementIsNotFound()
        {
            storage.Add(10);
            storage.Add(15);
            var t = storage.Get(5);
        }

        [TestMethod]
        public void AddingElementReturnsSameElement()
        {
            storage.Add(5);
            CollectionAssert.Contains(elements, 5);
        }
    }
}