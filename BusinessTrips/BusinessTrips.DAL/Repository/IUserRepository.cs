using System;
using BusinessTrips.DAL.Entity;

namespace BusinessTrips.DAL.Repository
{
    public interface IUserRepository
    {
        void CreateByUserEntity(UserEntity userEntity);
        UserEntity GetById(Guid userId);
        UserEntity GetByEmail(string userEmail);
        void CommitChanges();
    }
}
