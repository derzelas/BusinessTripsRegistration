﻿using System.Linq;

namespace BusinessTrips.DAL.Storage
{
    public interface IStorage
    {
        void Add<T>(T element) where T : class;

        IQueryable<T> GetStorageFor<T>() where T : class;

        void Remove<T>(T element) where T : class;

        void Commit();
    }
}