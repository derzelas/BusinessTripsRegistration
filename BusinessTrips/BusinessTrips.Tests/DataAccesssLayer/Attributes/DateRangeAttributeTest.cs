using System;
using BusinessTrips.DAL.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Attributes
{
    [TestClass]
    public class DateRangeAttributeTest
    {
        [TestMethod]
        public void Constructor_ValidateYearsBetweenLastYearAndNextTwentyYears()
        {
            var dataRange = new DateRangeAttribute();

            DateTime startingDate;
            DateTime.TryParse(dataRange.Minimum.ToString(), out startingDate);

            DateTime endingDate;
            DateTime.TryParse(dataRange.Maximum.ToString(), out endingDate);
            
            Assert.AreEqual(startingDate.Year, DateTime.Now.AddYears(-1).Year);
            Assert.AreEqual(endingDate.Year, DateTime.Now.AddYears(20).Year);
        }
    }
}
