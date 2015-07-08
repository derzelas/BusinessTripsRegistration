using System.Data.Entity;

namespace BusinessTrips.DAL.Storage
{
    public class EfStorageDbInitializer : DropCreateDatabaseIfModelChanges<EfStorage>
    {
    }
}