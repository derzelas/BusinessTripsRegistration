//using System.Linq;
//using BusinessTrips.DataAccessLayer;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//
//namespace BusinessTrips.Tests.DataAccesssLayer
//{
//    [TestClass]
//    public class InMemoryStorageTest
//    {
//        private InMemoryStorage storage;
//
//        [TestInitialize]
//        public void Initialize()
//        {
//            storage = new InMemoryStorage();
//        }
//
//        [TestMethod]
//        public void GetStorageForReturnsIQueryable()
//        {
//            storage.Add(10);
//            storage.Add(15);
//            var t = storage.GetSetFor();
//            Assert.IsInstanceOfType(t,typeof (IQueryable<int>));
//        }
//
//        [TestMethod]
//        public void AddedElementIsFoundInStorage()
//        {
//            //arrange
//            storage.Add(30);
//
//            //act
//            int expectedValue = storage.GetSetFor().First(i => i == 30);
//
//            //assert
//            Assert.AreEqual(30, expectedValue);
//        }
//    }
//}