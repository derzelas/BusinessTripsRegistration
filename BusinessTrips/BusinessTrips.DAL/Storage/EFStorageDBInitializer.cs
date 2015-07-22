using System.Data.Entity;
using BusinessTrips.DAL.Entity;

namespace BusinessTrips.DAL.Storage
{
    public class EfStorageDbInitializer : DropCreateDatabaseIfModelChanges<EfStorage>
    {
        protected override void Seed(EfStorage context)
        {
            context.Roles.Add(new RoleEntity { Id = (int)Roles.Regular, Name = Roles.Regular.ToString() });
            context.Roles.Add(new RoleEntity { Id = (int)Roles.Hr, Name = Roles.Hr.ToString() });

            base.Seed(context);
        }
    }
}