using System;
using BusinessTrips.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class InMemoryStorageTest
    {
        private InMemoryStorage<int> storage;

        [TestInitialize]
        public void Initialize()
        {
            storage = new InMemoryStorage<int>();
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void GetInexistentElementThrowsException()
        {
            storage.Add(10);
            storage.Add(15);
            storage.Get(7);
        }

        [TestMethod]
        public void AddedElementIsFoundInStorage()
        {
            storage.Add(30);
            int t = storage.Get(30);
            Assert.AreEqual(30, t);
        }
    }
}