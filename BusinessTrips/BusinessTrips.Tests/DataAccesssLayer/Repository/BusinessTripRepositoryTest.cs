using System;
using System.Data.Entity;
using System.Linq;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Model.BusinessTrip;
using BusinessTrips.DAL.Model.User;
using BusinessTrips.DAL.Repository;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Repository
{
    [TestClass]
    public class BusinessTripRepositoryTest
    {
        private BusinessTripsRepository repository;
        private BusinessTripModel businessTripModel;
        private UserRegistrationModel userRegistrationModel;
        private UserModel userModel;

        [TestInitialize]
        public void Initialize()
        {
            EfStorage storage = new EfStorage(new DropCreateDatabaseAlways<EfStorage>());
            storage.Database.Initialize(true);

            userRegistrationModel = new UserRegistrationModel()
            {
                Email = "email@email.email",
                Password = "password"
            };

            userRegistrationModel.Save();

            userModel=new UserModel(userRegistrationModel.Id);

            repository = new BusinessTripsRepository();
            businessTripModel = new BusinessTripModel
            {
                Id = Guid.NewGuid(),
                EndingDate = DateTime.Now,
                StartingDate = DateTime.Now,
                User = userModel
            };

            repository.CommitChanges();
        }

        [TestMethod]
        public void AddedElementIsFoundInStorageByHisId()
        {
            repository.Add(businessTripModel);
            repository.CommitChanges();

            var actual = new BusinessTripModel(repository.GetById(businessTripModel.Id));

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
                    User = userModel
                };

                repository.Add(businessTripModel);
            }

            repository.CommitChanges();

            var actual = repository.GetByUser(userRegistrationModel.Id).Select(e => new BusinessTripModel(e));

            foreach (var tripModel in actual)
            {
                Assert.AreEqual(userRegistrationModel.Id, tripModel.User.Id);
            }
        }
    }
}
