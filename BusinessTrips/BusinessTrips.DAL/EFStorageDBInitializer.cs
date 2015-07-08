using System.Data.Entity;

namespace BusinessTrips.DAL
{
    public class EfStorageDbInitializer : DropCreateDatabaseIfModelChanges<EfStorage>
    {
    }
}