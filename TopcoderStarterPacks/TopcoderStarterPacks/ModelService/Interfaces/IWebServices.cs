using Models.Interfaces;
using System.Collections.Generic;

namespace ModelServices.Interfaces
{
    interface IWebServices<T,IT> where T : class,IT, new() where IT : IModel
    {
        IEnumerable<T> Get();
        T Get(int id);
        int Post(IT obj);
        void Put(int id, IT obj);
        bool Delete(int id);
        bool Delete(IT obj);
    }
}
