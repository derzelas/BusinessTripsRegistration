using System.Data.Entity;
using BusinessTrips.DAL.Entities;
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
