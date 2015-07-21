using System.Data.Entity;
using System.Linq;
using BusinessTrips.DAL.Entity;

namespace BusinessTrips.DAL.Storage
{
    public class EfStorage : DbContext, IStorage
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<BusinessTripEntity> BusinessTrips { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        public EfStorage()
            : base("BusinessTrips")
        {
            Database.SetInitializer(new EfStorageDbInitializer());
        }

        public EfStorage(IDatabaseInitializer<EfStorage> initializer)
            : base("BusinessTrips")
        {
            Database.SetInitializer(initializer);
        }

        public void Add<T>(T entity) where T : class
        {
            var set = Set<T>();
            set.Add(entity);
        }

        public IQueryable<T> GetStorageFor<T>() where T : class
        {
            return Set<T>();
        }

        public void Commit()
        {
            SaveChanges();
        }
    }
}
