using Models;
using Repository.Persistance;
using Services.ServiceComponents.AlbumBehaviours;

namespace Services.ServiceComponents.AlbumsTypedBehaviour
{
    public class WebsiteAlbumPostBehaviour : IAlbumPostBehaviour
    {
        private RepositoryWorker _worker = null;
        public WebsiteAlbumPostBehaviour()
        {
            _worker = RepositoryWorker.Instance();
        }
        public WebsiteAlbumPostBehaviour(string DBCS)
        {
            _worker = RepositoryWorker.Instance(DBCS);
        }
        public int Post(Album entity)
        {
            _worker.Albums.Add(entity);
            _worker.Complete();
            return entity.Id;
        }
    }
}
