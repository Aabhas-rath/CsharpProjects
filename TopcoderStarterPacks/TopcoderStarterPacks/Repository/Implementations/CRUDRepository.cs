using System.Collections.Generic;
using System.Linq;
using Repository.Interfaces;
using Models.Interfaces;

namespace Repository.Implementations
{
    public abstract class CRUDRepository<T,IT> : ICrudRepository<T,IT> where T : class,IT, new() where IT : IModel
    {
        private DatabaseContext<T,IT> Context { get; set; }
        public CRUDRepository(string connectionString)
        {
                Context=new DatabaseContext<T,IT>(connectionString);
        }
        public void Save() {
            Context.SaveChanges();
        }

        public T Get(int id)
        {
            return Context.Models.Find(id);
        }

        public List<T> Getall()
        {
            return Context.Models.ToList();
        }

        public int Post(IT obj)
        {
            Context.Models.Add((T)obj);
            Context.SaveChanges();
            return obj.Id;
        }

        public T Put(int id, IT newObj)
        {
            T obj = Context.Models.Find(id);
            Context.Entry(obj).CurrentValues.SetValues(newObj);
            Context.SaveChanges();
            return obj;
        }

        public bool DeleteById(int id)
        {
            Context.Models.Remove(Get(id));
            Context.SaveChanges();
            return Context.Models.Find(id) != null;
        }

        public bool Delete(IT obj)
        {
            Context.Models.Remove((T)obj);
            Context.SaveChanges();
            return Context.Models.Contains((T)obj);
        }

        public long Count()
        {
            return Context.Models.LongCount();
        }

        public bool ExistsById(int id)
        {
            return Get(id) != null;
        }
    }
}
