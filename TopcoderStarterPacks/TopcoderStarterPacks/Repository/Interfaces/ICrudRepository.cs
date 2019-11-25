using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface ICrudRepository<T,IT> where T : class, IT, new()
    {
        T Get(int id);
        List<T> Getall();
        int Post(IT obj);
        T Put(int id, IT newObj);
        bool DeleteById(int id);
        bool Delete(IT obj);
        long Count();
        bool ExistsById(int id);
        void Save();
    }
}
