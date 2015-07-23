using System;
using BusinessTrips.DAL.Entity;

namespace BusinessTrips.DAL.Repository
{
    public interface IUserRepository
    {
        void Add(UserEntity userEntity);
        UserEntity GetBy(Guid userId);
        UserEntity GetBy(string userEmail);
        void SaveChanges();
    }
}
