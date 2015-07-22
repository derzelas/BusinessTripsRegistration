using System.Data.Entity;
using BusinessTrips.DAL.Entity;
using BusinessTrips.DAL.Storage;

namespace BusinessTrips.Tests
{
    public class EfStorageDbInitializerTest : DropCreateDatabaseAlways<EfStorage>
    {
        protected override void Seed(EfStorage context)
        {
            context.Roles.Add(new RoleEntity { Name = "Regular" });
            context.Roles.Add(new RoleEntity { Name = "HR" });

            base.Seed(context);
        }
    }
}
