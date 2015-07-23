using System;
using BusinessTrips.DAL.Model.BusinessTrip;
using BusinessTrips.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Models
{
    [TestClass]
    public class BusinessTripCollectionModelTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            // waiting for refactoring businesstripfilter and BusinessTripBiewModel
            var businessTripModel = new BusinessTripModel()
            {
                StartingDate = DateTime.Now,
                Accomodation = "accmomodation",
                ClientLocation = "new city",
                ClientName = "John",
            };

            var businessTripsRepository = new BusinessTripsRepository();
            businessTripsRepository.Add(businessTripModel);
        }
    }
}
