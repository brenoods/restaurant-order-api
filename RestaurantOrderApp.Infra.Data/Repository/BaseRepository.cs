using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderApp.Domain.Interfaces;

namespace RestaurantOrderApp.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly RestauranteOrderAppContext db;
        protected readonly DbSet<TEntity> dbSet;

        public BaseRepository(RestauranteOrderAppContext context)
        {
            db = context;
            dbSet = db.Set<TEntity>();
        }
        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Add(List<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            var entityToRemove = dbSet.Find(id);
            if (entityToRemove != null)
                Remove(entityToRemove);
        }

        public virtual void Remove(TEntity entityToRemove)
        {
            if (db.Entry(entityToRemove).State == EntityState.Detached)
                dbSet.Attach(entityToRemove);
            dbSet.Remove(entityToRemove);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            db.Entry(entityToUpdate).State = EntityState.Modified;
        }

    }
}
