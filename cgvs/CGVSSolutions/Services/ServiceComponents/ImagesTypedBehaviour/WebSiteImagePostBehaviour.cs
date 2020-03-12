using Models;
using Repository.Persistance;
using Services.ServiceComponents.ImageBehaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceComponents.ImagesTypedBehaviour
{
    class WebSiteImagePostBehaviour : IImagePostBehaviour
    {
        private RepositoryWorker _worker = null;

        public WebSiteImagePostBehaviour()
        {
            _worker = new RepositoryWorker();
        }
        public WebSiteImagePostBehaviour(string DBCS)
        {
            _worker = new RepositoryWorker(DBCS);
        }
        public int Post(Image entity)
        {
            _worker.Images.Add(entity);
            _worker.Complete();
            return entity.Id;
        }
    }
}
