using Repository.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private static Repository<TEntity> _instance = null;
        private static readonly object syncLock = new object();

        protected readonly DbContext Context;
        private DbSet<TEntity> _entity = null; 

        protected Repository(DbContext context)
        {
            Context = context;
            _entity = context.Set<TEntity>();
            _entity.Load();
        }

        protected static Repository<TEntity> repository(DbContext DBContext)
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Repository<TEntity>(DBContext);
                    }
                }
            }
            return _instance;
        }
        public TEntity Get(int id)
        {
            // Here we are working with a DbContext, not PlutoContext. So we don't have DbSets 
            // such as Courses or Authors, and we need to use the generic Set() method to access them.
            return _entity.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entity.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.Where(predicate).ToList();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            _entity.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entity.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _entity.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entity.RemoveRange(entities);
        }
        public void Update(TEntity entity)
        {
            _entity.Attach(entity);
            var entry = Context.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}
