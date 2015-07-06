using System;
using System.Linq;
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
        public void GetStorageForReturnsIQueryable()
        {
            storage.Add(10);
            storage.Add(15);
            var t = storage.GetStorageFor();
            Assert.IsInstanceOfType(t,typeof (IQueryable<int>));
        }

        [TestMethod]
        public void AddedElementIsFoundInStorage()
        {
            storage.Add(30);
            int t = storage.GetStorageFor().First(i => i == 30);
            Assert.AreEqual(30, t);
        }
    }
}