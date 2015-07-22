using System.Data.Entity;
using BusinessTrips.DAL.Entity;

namespace BusinessTrips.DAL.Storage
{
    public class EfStorageDbInitializer : DropCreateDatabaseIfModelChanges<EfStorage>
    {
        protected override void Seed(EfStorage context)
        {
            context.Roles.Add(new RoleEntity { Id = (int)Role.Regular, Name = Role.Regular.ToString() });
            context.Roles.Add(new RoleEntity { Id = (int)Role.Hr, Name = Role.Hr.ToString() });

            base.Seed(context);
        }
    }
}