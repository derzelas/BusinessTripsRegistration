using System.Web.Mvc;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.DAL.Attribute
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public RoleAuthorizeAttribute(params Roles[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
