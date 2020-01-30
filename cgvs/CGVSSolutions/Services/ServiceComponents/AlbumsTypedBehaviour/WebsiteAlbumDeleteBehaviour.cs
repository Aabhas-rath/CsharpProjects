using Models;
using Repository.Persistance;
using Services.ServiceComponents.AlbumBehaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceComponents.AlbumsTypedBehaviour
{
    public class WebsiteAlbumDeleteBehaviour : IAlbumDeleteBehaviour
    {
        private RepositoryWorker _worker = null;
        public WebsiteAlbumDeleteBehaviour()
        {
            _worker = new RepositoryWorker();
        }
        public WebsiteAlbumDeleteBehaviour(string DBCS)
        {
            _worker = new RepositoryWorker(DBCS);
        }
        public bool Delete(Album entity)
        {
            int id = entity.Id;
            _worker.Albums.Remove(entity);
            _worker.Complete();
            var newAlbum = _worker.Albums.Get(id);
            return newAlbum == null;
        }
    }
}
