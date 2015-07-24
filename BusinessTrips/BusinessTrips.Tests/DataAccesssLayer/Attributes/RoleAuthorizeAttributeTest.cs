using BusinessTrips.DAL.Attributes;
using BusinessTrips.DAL.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTrips.Tests.DataAccesssLayer.Attributes
{
    [TestClass]
    public class RoleAuthorizeAttributeTest
    {
        [TestMethod]
        public void Constructor_MultipleParameters_MergesThemSeparatedByCommaInSingleString()
        {
            var authorizeAttribute = new RoleAuthorizeAttribute(Role.Regular, Role.Hr);

            Assert.AreEqual("Regular,Hr", authorizeAttribute.Roles);
        }
    }
}
