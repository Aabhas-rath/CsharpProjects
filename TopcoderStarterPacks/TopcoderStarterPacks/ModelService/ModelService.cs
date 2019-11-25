using Models;
using Models.Interfaces;
using ModelServices.Interfaces;
using Repository.Implementations;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace ModelServices
{
    public class ModelService : IWebServices<Models.Model,IModel>
    {
        private ModelRepository repos;
        public ModelService(IModelRepository repository)
        {
            repos = (ModelRepository)repository;
        }

        public bool Delete(int id)
        {
           return repos.DeleteById(id);
        }

        public bool Delete(IModel obj)
        {
            return repos.Delete(obj);
        }

        public IEnumerable<Model> Get()
        {
           return repos.Getall();
        }

        public Model Get(int id)
        {
            return repos.Get(id);
        }

        public int Post(IModel obj)
        {
            return repos.Post(obj);
        }

        public void Put(int id,IModel obj)
        {
            repos.Put(id, obj);
        }
    }
}
