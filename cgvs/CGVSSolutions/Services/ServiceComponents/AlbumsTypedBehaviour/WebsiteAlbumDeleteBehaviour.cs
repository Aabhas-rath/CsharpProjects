using Models;
using Repository.Persistance;
using Services.ServiceComponents.AlbumBehaviours;

namespace Services.ServiceComponents.AlbumsTypedBehaviour
{
    public class WebsiteAlbumDeleteBehaviour : IAlbumDeleteBehaviour
    {
        private RepositoryWorker _worker = null;
        public WebsiteAlbumDeleteBehaviour()
        {
            _worker = RepositoryWorker.Instance();
        }
        public WebsiteAlbumDeleteBehaviour(string DBCS)
        {
            _worker = RepositoryWorker.Instance(DBCS);
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
