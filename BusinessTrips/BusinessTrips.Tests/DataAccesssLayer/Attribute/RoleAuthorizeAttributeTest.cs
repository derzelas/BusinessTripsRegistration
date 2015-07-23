using System;
using BusinessTrips.DAL.Attribute;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Attribute
{
    [TestClass]
    public class RoleAuthorizeAttributeTest
    {
        [TestMethod]
        public void Constructor_MultipleParameters_MergesThemSeparatedByCommaInSingleString()
        {
            RoleAuthorizeAttribute authorizeAttribute = new RoleAuthorizeAttribute(Role.Regular, Role.Hr);

            Assert.AreEqual("Regular,Hr", authorizeAttribute.Roles);
        }
    }
}
