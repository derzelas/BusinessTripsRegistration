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
            storage = InMemoryStorage<int>.GetInstace(elements);
        }

        [TestMethod]
        public void AnInstanceIsCreated()
        {
            Assert.IsNotNull(storage);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void GetInexistentElementThrowsException()
        {
            storage.Add(10);
            storage.Add(15);
            var t = storage.Get(7);
        }

        [TestMethod]
        public void GetExistentElementIsFound()
        {
            storage.Add(30);
            var t = storage.Get(30);
            Assert.AreEqual(30, t);
        }

        [TestMethod]
        public void AddedElementIsInStorge()
        {
            storage.Add(5);
            CollectionAssert.Contains(elements, 5);
        }
    }
}