using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        T Get(int id);
        IEnumerable<T> Getall();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T Entity);
        void AddRange(IEnumerable<T> Entities);

        void Remove(T Entity);
        void RemoveAll(IEnumerable<T> Entities);

    }
}
