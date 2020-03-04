using Models;
using Repository.Persistance;
using Services.ServiceComponents.AlbumBehaviours;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Services.ServiceComponents.AlbumsTypedBehaviour
{
    public class WebsiteAlbumGetBehaviour : IAlbumGetBehaviour
    {
        private RepositoryWorker _worker = null;
        public WebsiteAlbumGetBehaviour()
        {
            _worker = RepositoryWorker.Instance();
        }
        public WebsiteAlbumGetBehaviour(string DBCS)
        {
            _worker = RepositoryWorker.Instance(DBCS);
        }
        public IEnumerable<Album> Find(Expression<Func<Album, bool>> predicate)
        {
            return _worker.Albums.Find(predicate);
        }

        public Album Get(int id)
        {
            return _worker.Albums.Get(id);
        }

        public IEnumerable<Album> GetAll()
        {
            return _worker.Albums.GetAll();
        }

        public IEnumerable<Image> GetAllImagesAssociated(int id)
        {
            return Get(id).Images;
        }

        public string GetFolderOfAlbum(int id)
        {
            return Get(id).FolderPath;
        }

        public int GetNumberOfImagesInAlbum(int id)
        {
            return Get(id).Images.Count;
        }

    }
}
