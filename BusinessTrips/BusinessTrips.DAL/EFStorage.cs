using System.Data.Entity;
using System.Linq;

namespace BusinessTrips.DAL
{
    public class EFStorage : DbContext, IStorage
    {
        public EFStorage()
            : base("BusinessTrips")
        {
            Database.SetInitializer(new EFStorageDBInitializer());
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
            throw new System.NotImplementedException();
        }

        public void Commit()
        {
            SaveChanges();
        }
    }
}
