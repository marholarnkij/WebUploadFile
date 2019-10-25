using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Lib.Contract;
using Common.Lib.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Lib.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext ObjectContext;
        protected readonly DbSet<TEntity> ObjectSet;
        public Repository(DbContext context)
        {
            this.ObjectContext = context;
            this.ObjectSet = context.Set<TEntity>();
        }

        //public virtual TEntity Get(int id)
        //{
        //    return Get(id, false);
        //}

        //public virtual TEntity Get(int id, bool fromSource)
        //{
        //    if (fromSource)
        //    {
        //        return ObjectSet.AsNoTracking().FirstOrDefault(e => e.Id.Equals(id));
        //    }
        //    return ObjectSet.Find(id);
        //}

        public virtual IQueryable<TEntity> GetAll()
        {
            return GetAll(e => true);
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return ObjectSet.Where(predicate);
        }

        public virtual void Create(TEntity item)
        {
            ObjectSet.Add(item);
        }
        //public virtual void Delete(int id)
        //{
        //    var item = Get(id);
        //    Delete(item);
        //}

        public virtual void Delete(TEntity entityToDelete)
        {
            if (ObjectContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                ObjectSet.Attach(entityToDelete);
            }
            ObjectSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            ObjectSet.Attach(entityToUpdate);
            ObjectContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return ObjectSet.FirstOrDefault(predicate);
        }
    }
}
