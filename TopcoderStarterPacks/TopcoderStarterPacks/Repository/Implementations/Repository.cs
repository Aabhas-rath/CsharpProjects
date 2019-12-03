using System.Collections.Generic;
using System.Linq;
using Repository.Interfaces;
using Models.Interfaces;
using System;
using System.Linq.Expressions;
using MySql.Data.EntityFramework;
using System.Data.Entity;

namespace Repository.Implementations
{
    public abstract class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly DbContext context;
        public Repository(string connectionString)
        {
            DbContext dbContext = new DbContext(connectionString);
            context = dbContext;
        }
        public void Add(T Entity)
        {
            context.Set<T>().Add(Entity);
        }

        public void AddRange(IEnumerable<T> Entities)
        {
            context.Set<T>().AddRange(Entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public IEnumerable<T> Getall()
        {
            return context.Set<T>().ToList();
        }

        public void Remove(T Entity)
        {
            if (context.Set<T>().Contains(Entity))
            {
                context.Set<T>().Remove(Entity);
            }
            else
            {
                throw new NullReferenceException($"{Entity.ToString()} does not exists in Data base");
            }
        }

        public void RemoveAll(IEnumerable<T> Entities)
        {
            context.Set<T>().RemoveRange(Entities);
        }
    }
}
