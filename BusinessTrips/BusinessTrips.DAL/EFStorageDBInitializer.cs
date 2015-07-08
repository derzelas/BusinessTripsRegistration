using System.Data.Entity;

namespace BusinessTrips.DAL
{
    public class EFStorageDBInitializer : DropCreateDatabaseIfModelChanges<EFStorage>
    {
    }
}