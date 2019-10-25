using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
namespace Common.Lib.Contract
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        //TEntity Get(int id);
        //TEntity Get(int id, bool fromSource);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        void Create(TEntity item);
        //void Delete(int id);
        void Update(TEntity entityToUpdate);

    }
}
