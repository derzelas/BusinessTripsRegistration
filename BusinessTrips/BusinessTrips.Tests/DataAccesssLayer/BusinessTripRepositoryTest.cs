using System;
using System.Data.Entity;
using BusinessTrips.DAL.Entity;
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
        private UserEntity userEntity;

        [TestInitialize]
        public void Initialize()
        {
            EfStorage storage = new EfStorage(new DropCreateDatabaseAlways<EfStorage>());
            storage.Database.Initialize(true);

            userEntity = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Email = "email@email.email",
                HashedPassword = "password"
            };

            repository = new BusinessTripsRepository();
            businessTripModel = new BusinessTripModel
            {
                Id = Guid.NewGuid(),
                EndingDate = DateTime.Now,
                StartingDate = DateTime.Now,
                User = userEntity
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
                    User = userEntity
                };
                repository.Add(businessTripModel);
            }

            repository.CommitChanges();

            var actual = repository.GetByUser(userEntity.Id);

            foreach (var tripModel in actual)
            {
                Assert.AreEqual(userEntity.Id, tripModel.User.Id);
            }
        }
    }
}
