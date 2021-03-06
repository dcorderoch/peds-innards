﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace MyLearnDAL.Repositories
{
    /// <summary>
    /// Generic repository. It provides CRUD operations to be inheited by custom repositories.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IDisposable where T : class
    {
        private bool disposed = false;
        private MyLearnContext context = null;

        protected DbSet<T> DbSet
        {
            get; set;
        }

        public Repository()
        {
            context = new MyLearnContext();
            DbSet = context.Set<T>();
        }

        public Repository(MyLearnContext context)
        {
            this.context = context;
            DbSet = context.Set<T>();
        }
        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public T Add(T entity)
        {
            return DbSet.Add(entity);
        }

        public T Attach(T entity)
        {
            return DbSet.Attach(entity);
        }

        public virtual void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                context.Dispose();
                disposed = true;
            }
        }
    }

}