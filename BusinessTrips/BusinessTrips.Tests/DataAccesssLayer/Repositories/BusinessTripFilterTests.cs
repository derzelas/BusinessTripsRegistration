using System;
using System.Linq;
using BusinessTrips.DAL.Entities;
using BusinessTrips.DAL.Models.BusinessTrip;
using BusinessTrips.DAL.Models.User;
using BusinessTrips.DAL.Repositories;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Repositories
{
    [TestClass]
    public class BusinessTripFilterTests
    {
        private BusinessTripFilter businessTripFilter;
        private BusinessTripsRepository businessTripsRepository;        

        [TestInitialize]
        public void TestInitialize()
        {
            EfStorage storage = new EfStorage(new EfStorageDbInitializerTest());
            storage.Database.Initialize(true);

            businessTripFilter = new BusinessTripFilter();
            businessTripsRepository = new BusinessTripsRepository();
        }

        private const int NumberOfBusinessTrips = 2;

        [TestMethod]
        public void GetBusinessTripsBy_MultipleBusinessTripsNoFilterValue_ReturnsAllBusinessTrips()
        {
            AddGeneratedBusinessTripsWithConstraintsBy(NumberOfBusinessTrips, new BusinessTripFilter());

            var actualBusinessTripCollection = businessTripsRepository.GetBusinessTripsBy(new BusinessTripFilter());

            Assert.AreEqual(NumberOfBusinessTrips, actualBusinessTripCollection.Count());
        }

        [TestMethod]
        public void GetBusinessTripBy_MultipleBusinessTripsSameStartingDate_ReturnsAllBusinessTrips()
        {
            businessTripFilter.StartingDate = DateTime.Now;

            AddGeneratedBusinessTripsWithConstraintsBy(NumberOfBusinessTrips, businessTripFilter);

            var actualCollection = businessTripsRepository.GetBusinessTripsBy(businessTripFilter);

            Assert.AreEqual(NumberOfBusinessTrips, actualCollection.Count());
        }

        [TestMethod]
        public void GetBusinessTripBy_MultipleBusinessTripsSameEndingDate_ReturnsAllBusinessTrips()
        {
            businessTripFilter.EndingDate = DateTime.Now.AddYears(3);

            AddGeneratedBusinessTripsWithConstraintsBy(NumberOfBusinessTrips, businessTripFilter);

            var actualCollection = businessTripsRepository.GetBusinessTripsBy(businessTripFilter);

            Assert.AreEqual(NumberOfBusinessTrips, actualCollection.Count());
        }

        [TestMethod]
        public void GetBusinessTripBy_MultipleBusinessTripsSameLocation_ReturnsAllBusinessTrips()
        {
            businessTripFilter.Location = "MyLocation";

            AddGeneratedBusinessTripsWithConstraintsBy(NumberOfBusinessTrips, businessTripFilter);

            var actualCollection = businessTripsRepository.GetBusinessTripsBy(businessTripFilter);

            Assert.AreEqual(NumberOfBusinessTrips, actualCollection.Count());
        }

        [TestMethod]
        public void GetBusinessTripBy_MultipleBusinessTripsSameMeansOfTransportation_ReturnsAllBusinessTrips()
        {
            businessTripFilter.MeansOfTransportation = "My mean of transportation";

            AddGeneratedBusinessTripsWithConstraintsBy(NumberOfBusinessTrips, businessTripFilter);

            var actualCollection = businessTripsRepository.GetBusinessTripsBy(businessTripFilter);

            Assert.AreEqual(NumberOfBusinessTrips, actualCollection.Count());
        }

        [TestMethod]
        public void GetBusinessTripBy_MultipleBusinessTripsSameAccommodation_ReturnsAllBusinessTrips()
        {
            businessTripFilter.Accommodation = "My accomodation";

            AddGeneratedBusinessTripsWithConstraintsBy(NumberOfBusinessTrips, businessTripFilter);

            var actualCollection = businessTripsRepository.GetBusinessTripsBy(businessTripFilter);

            Assert.AreEqual(NumberOfBusinessTrips, actualCollection.Count());
        }

        private void AddGeneratedBusinessTripsWithConstraintsBy(int numberOfBusinessTrips, BusinessTripFilter filter)
        {
            Random random = new Random();

            var userRepository = new UserRepository();

            for (int i = 0; i < numberOfBusinessTrips; i++)
            {
                var userEntity = new UserEntity
                {
                    Id = Guid.NewGuid()
                };

                userRepository.Add(userEntity);
                userRepository.SaveChanges();

                var businessTrip = new BusinessTripModel();

                businessTrip.StartingDate = filter.StartingDate.HasValue
                    ? filter.StartingDate.GetValueOrDefault()
                    : DateTime.Now.AddYears(random.Next(-1, 1));

                businessTrip.EndingDate = filter.EndingDate.HasValue
                    ? filter.EndingDate.GetValueOrDefault()
                    : DateTime.Now.AddYears(random.Next(-1, 1));

                businessTrip.ClientLocation = !string.IsNullOrEmpty(filter.Location)
                    ? filter.Location
                    : "Locations" + random.Next(0, numberOfBusinessTrips);

                businessTrip.MeansOfTransportation = !string.IsNullOrEmpty(filter.MeansOfTransportation)
                    ? filter.MeansOfTransportation
                    : "MeansOfTransportations" + random.Next(0, numberOfBusinessTrips);

                businessTrip.Accomodation = !string.IsNullOrEmpty(filter.Accommodation)
                    ? filter.Accommodation
                    : "Accomodation" + random.Next(0, numberOfBusinessTrips);


                businessTrip.User = new UserModel();

                businessTrip.User.Id = !string.IsNullOrEmpty(filter.UserId)
                    ? Guid.Parse(filter.UserId)
                    : userEntity.Id;

                businessTripsRepository.Add(businessTrip);
            }

            businessTripsRepository.SaveChanges();
        }
    }
}
