using System.Data.Entity;
using System.Linq;
using BusinessTrips.DAL.Entity;

namespace BusinessTrips.DAL.Storage
{
    public class EfStorage : DbContext, IStorage
    {
        public EfStorage()
            : base("BusinessTrips")
        {
            Database.SetInitializer(new EfStorageDbInitializer());
        }

        public DbSet<UserEntity> Users { get; set; }

        public void Add<T>(T entity) where T : class
        {
            var set = Set<T>();
            set.Add(entity);
        }

        public IQueryable<T> GetStorageFor<T>() where T : class
        {
            return Set<T>();
        }

        public void Remove<T>(T element) where T : class
        {
            Set<T>().Remove(element);
        }

        public void Commit()
        {
            SaveChanges();
        }
    }
}
