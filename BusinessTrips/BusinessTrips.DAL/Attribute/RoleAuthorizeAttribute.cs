using System.Web.Mvc;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Attribute
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public RoleAuthorizeAttribute(params Role[] role)
        {
            Roles = string.Join(",", role);
        }
    }
}
