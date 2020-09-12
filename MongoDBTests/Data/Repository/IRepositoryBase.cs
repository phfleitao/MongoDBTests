using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoDBTests.Data.Repository
{
    public interface IRepositoryBase<TEntity>
    {
        void Save(TEntity entity);
        void Delete(Guid id);

        IEnumerable<TEntity> SelectAll();
        IEnumerable<TEntity> SelectWhere(Expression<Func<TEntity, bool>> predicate);
        TEntity SelectById(Guid id);
        TEntity SelectOne(Expression<Func<TEntity, bool>> predicate);
    }
}
