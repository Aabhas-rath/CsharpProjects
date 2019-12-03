using Models.Interfaces;
using System.Collections.Generic;

namespace ModelServices.Interfaces
{
    interface IWebServices<T> where T : class, new() 
    {
        IEnumerable<T> Get();
        T Get(int id);
        int Post(T obj);
        void Put(int id, T obj);
        bool Delete(int id);
        bool Delete(T obj);
    }
}
