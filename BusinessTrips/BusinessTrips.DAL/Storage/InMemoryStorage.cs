﻿using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTrips.DAL.Entity;

namespace BusinessTrips.DAL.Storage
{
    public class InMemoryStorage : IStorage
    {
        private List<UserEntity> storage = new List<UserEntity>();

        public void Add<T>(T obj) where T : class
        {
            storage.Add(obj as UserEntity);
        }

        public IQueryable<T> GetSetFor<T>() where T : class
        {
            return storage.AsQueryable().Cast<T>();
        }

        public void Remove<T>(T element) where T : class
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}