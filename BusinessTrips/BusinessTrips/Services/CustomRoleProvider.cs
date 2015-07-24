using System;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Security;
using BusinessTrips.DAL.Repositories;

namespace BusinessTrips.Services
{
    public class CustomRoleProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ProviderException("Email cannot be empty or null.");
            }

            Guid parsedGuid;
            if (Guid.TryParse(id, out parsedGuid))
            {
                var userRepository = new UserRepository();
                var userEntity = userRepository.GetBy(parsedGuid);

                return userEntity.Roles.Select(x => x.Name).ToArray();
            }
            return null;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}