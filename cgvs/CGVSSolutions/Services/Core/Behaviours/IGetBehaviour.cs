using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Services.Core.Behaviours
{
    public interface IGetBehaviour<TEntity> where TEntity :class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
