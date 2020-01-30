using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models;
using Repository.Persistance;
using Services.ServiceComponents.ImageBehaviours;

namespace Services.ServiceComponents.ImageTypedBehaviour
{
    public class WebSiteImageGetBehaviour : IImageGetBehaviour
    {
        private RepositoryWorker _repository =null;

        public WebSiteImageGetBehaviour()
        {
            _repository = new RepositoryWorker();
        }
        public WebSiteImageGetBehaviour(string DBCS)
        {
            _repository = new RepositoryWorker(DBCS);
        }
        public IEnumerable<Image> Find(Expression<Func<Image, bool>> predicate)
        {
            return _repository.Images.Find(predicate);
        }

        public Image Get(int id)
        {
            return _repository.Images.Get(id);
        }

        public IEnumerable<Image> GetAll()
        {
            return _repository.Images.GetAll();
        }

        public string GetPathOfImage(int id)
        {
            return _repository.Images.GetPathOfImage(id);
        }

        public string GetPathOfImage(int id, int Version)
        {
            return _repository.Images.GetPathOfImage(id,Version);
        }

        public Dictionary<int, string> GetPathsOfAllVersionsOfImage(int id)
        {
            return _repository.Images.GetPathsOfAllVersionsOfImage(id);
        }
    }
}
