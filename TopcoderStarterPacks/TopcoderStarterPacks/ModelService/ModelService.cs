using Models;
using Models.Interfaces;
using ModelServices.Interfaces;
using Repository.Implementations;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace ModelServices
{
    public class ModelService : IWebServices<Models.Model>
    {
        private ModelRepository repos;
        public ModelService(IModelRepository repository)
        {
            repos = (ModelRepository)repository;
        }

        public bool Delete(int id)
        {
            try
            {
                repos.Remove(repos.Get(id));
            }
            catch (NullReferenceException e)
            {
                return false;
            }
            return true;
        }

        public bool Delete(Model obj)
        {
            try
            {
                repos.Remove(obj);
            }
            catch (NullReferenceException e)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Model> Get()
        {
           return repos.Getall();
        }

        public Model Get(int id)
        {
            return repos.Get(id);
        }

        public int Post(Model obj)
        {
            repos.Add(obj);
            return obj.Id;
        }

        public void Put(int id,IModel obj)
        {
            var sobj = repos.Get(id);

        }
    }
}
