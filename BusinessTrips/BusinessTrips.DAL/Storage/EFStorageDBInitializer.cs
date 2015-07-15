using System.Data.Entity;
using BusinessTrips.DAL.Entity;

namespace BusinessTrips.DAL.Storage
{
    public class EfStorageDbInitializer : DropCreateDatabaseIfModelChanges<EfStorage>
    {
        protected override void Seed(EfStorage context)
        {
            context.Roles.Add(new RoleEntity {Name = "Regular"});
            context.Roles.Add(new RoleEntity {Name = "HR"});

            base.Seed(context);
        }
    }
}