using System.Data.Entity;
using BusinessTrips.DAL.Attribute;
using BusinessTrips.DAL.Model;
using BusinessTrips.DAL.Model.User;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer
{
    [TestClass]
    public class UniqueEmailAttributeTest
    {
        private UniqueEmailAttribute uniqueEmailAttribute;
        private EfStorage efStorage;

        [TestInitialize]
        public void Initialize()
        {
            efStorage = new EfStorage(new EfStorageDbInitializerTest());
            efStorage.Database.Initialize(true);

            uniqueEmailAttribute = new UniqueEmailAttribute();

            UserRegistrationModel userRegistrationModel = new UserRegistrationModel()
            {
                Name = "nume",
                Email = "email@email.com",
                Password = "password",
            };

            userRegistrationModel.Save();
        }

        [TestMethod]
        public void IsValidReturnsFalseForExistingEmail()
        {
            Assert.AreEqual(false, uniqueEmailAttribute.IsValid("email@email.com"));
        }

        [TestMethod]
        public void IsValidReturnsTrueForNonExistingEmail()
        {
            Assert.AreEqual(true, uniqueEmailAttribute.IsValid("emaifgdfgdfgdfgdfl@email.com"));
        }
    }
}
