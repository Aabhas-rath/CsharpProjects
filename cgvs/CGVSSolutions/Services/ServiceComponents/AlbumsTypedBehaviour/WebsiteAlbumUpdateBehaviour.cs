using Models;
using Repository.Persistance;
using Services.ServiceComponents.AlbumBehaviours;
using Services.ServiceComponents.ServiceExceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceComponents.AlbumsTypedBehaviour
{
    public class WebsiteAlbumUpdateBehaviour : WebsiteAlbumPostBehaviour, IAlbumUpdateBehaviour
    {
        private RepositoryWorker _worker = null;
        public WebsiteAlbumUpdateBehaviour()
        {
            _worker = new RepositoryWorker();
        }
        public WebsiteAlbumUpdateBehaviour(string DBCS)
        {
            _worker = new RepositoryWorker(DBCS);
        }

        public int AddImageToTheAlbum(int AlbumId, int ImageId)
        {
            var album = _worker.Albums.Get(AlbumId);
            var image = _worker.Images.Get(ImageId);
            album.Images.Add(image);
            _worker.Albums.Update(album);
            _worker.Complete();
            return _worker.Albums.Count();
        }

        public int AddImageToTheAlbum(int AlbumId, string ImagePath)
        {
            var album = _worker.Albums.Get(AlbumId);
            var image = new Image();
            image.Path = album.FolderPath +"\\"+ Path.GetFileName(ImagePath);
            image.Version = 1;
            if (File.Exists(ImagePath))
            {
                if (ImagePath != image.Path)
                {
                    File.Copy(ImagePath, image.Path);
                }
                album.Images.Add(image);
                _worker.Albums.Update(album);
                _worker.Complete();
                return _worker.Albums.Count();
            }
            else
            {
                throw new ImageNotFoundException("Image not found",ImagePath);
            }

        }

        public int DeleteImageFromAlbum(int AlbumId, int ImageId)
        {
            var album = _worker.Albums.Get(AlbumId);
            var image = _worker.Images.Get(ImageId);
            album.Images.Remove(image);
            _worker.Albums.Update(album);
            _worker.Complete();
            return _worker.Albums.Count();
        }

        public bool Update(Album oldEntity, Album newEntity)
        {
            try
            {
                if (newEntity.Id == 0)
                {
                    newEntity.Id = oldEntity.Id;
                }
                _worker.Albums.Update(newEntity);
                _worker.Complete();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
