using System;
using System.Data.Entity;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class BusinessTripRepositoryTest
    {
        private BusinessTripsRepository repository;
        private BusinessTripModel businessTripModel;
        private UserModel userModel;

        [TestInitialize]
        public void Initialize()
        {
            EfStorage storage = new EfStorage(new DropCreateDatabaseAlways<EfStorage>());
            storage.Database.Initialize(true);

            userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                Email = "email@email.email",
                Password = "password"
            };

            repository = new BusinessTripsRepository();
            businessTripModel = new BusinessTripModel
            {
                Id = Guid.NewGuid(),
                EndingDate = DateTime.Now,
                StartingDate = DateTime.Now,
                UserId = userModel.Id
            };
        }

        [TestMethod]
        public void AddedElementIsFoundInStorageByHisId()
        {
            repository.Add(businessTripModel);
            repository.CommitChanges();

            var actual = repository.GetById(businessTripModel.Id);

            Assert.AreEqual(businessTripModel.Id, actual.Id);
        }

        [TestMethod]
        public void ElementsAreFoundInStorageByUser()
        {
            for (int i = 0; i < 10; i++)
            {
                businessTripModel = new BusinessTripModel
                {
                    Id = Guid.NewGuid(),
                    EndingDate = DateTime.Now,
                    StartingDate = DateTime.Now,
                    UserId = userModel
                };
                repository.Add(businessTripModel);
            }

            repository.CommitChanges();

            var actual = repository.GetByUser(userModel.Id);

            foreach (var tripModel in actual)
            {
                Assert.AreEqual(userModel.Id, tripModel.UserId.Id);
            }
        }
    }
}
