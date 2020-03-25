using Services.ServiceComponents.AlbumBehaviours;
using Services.ServiceComponents.ServiceExceptions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public class AlbumService
    {
        public IAlbumGetBehaviour getBehaviour;
        public IAlbumDeleteBehaviour deleteBehaviour;
        public IAlbumPostBehaviour postBehaviour;
        public IAlbumUpdateBehaviour updateBehaviour;
        public AlbumService(IAlbumGetBehaviour GetBehaviour, IAlbumPostBehaviour PostBehaviour = null, IAlbumUpdateBehaviour UpdateBehaviour = null, IAlbumDeleteBehaviour DeleteBehaviour = null)
        {
            getBehaviour = GetBehaviour;
            deleteBehaviour = DeleteBehaviour;
            postBehaviour = PostBehaviour;
            updateBehaviour = UpdateBehaviour;
        }
        /// <summary>
        /// Fetches complete relative path of the folder
        /// </summary>
        /// <param name="id">id of album</param>
        /// <returns> complete relative path of the folder</returns>
        public string GetAlbumPath(int id)
        {
            var album = getBehaviour.Get(id);
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }
            else
            {
                return album.FolderPath;
            }
        }
        public string GetAlbumPath(string name)
        {
            var album = getBehaviour.Find(a=>a.Name==name).FirstOrDefault();
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }
            else
            {
                return album.FolderPath;
            }
        }
        /// <summary>
        /// list of paths of images
        /// </summary>
        /// <param name="id">album id</param>
        /// <returns>Dictionary of paths sorted by ids</returns>
        public IEnumerable<KeyValuePair<int,string>> GetImagePathsAssociatedToAlbum(int id)
        {
            var album = getBehaviour.Get(id);
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }
            else
            {
                var imagepathlist = new Dictionary<int, string>(album.Images.Count);
                foreach (var item in album.Images)
                {
                    imagepathlist.Add(item.Id, item.Path);
                }
                return imagepathlist;
            }
        }
        public IEnumerable<KeyValuePair<int, string>> GetImagePathsAssociatedToAlbum(string name)
        {
            var album = getBehaviour.Find(a=>a.Name==name).FirstOrDefault();
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }
            else
            {
                var imagepathlist = new Dictionary<int, string>(album.Images.Count);
                foreach (var item in album.Images)
                {
                    imagepathlist.Add(item.Id, item.Path);
                }
                return imagepathlist;
            }
        }
        /// <summary>
        /// list of Ids of images associated
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<int> GetImageIdsAssociatedToAlbum(int id)
        {
            var album = getBehaviour.Get(id);
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }
            else
            {
                var imagepathlist = new List<int>(album.Images.Count);
                foreach (var item in album.Images)
                {
                    imagepathlist.Add(item.Id);
                }
                return imagepathlist;
            }
        }
        public IEnumerable<int> GetImageIdsAssociatedToAlbum(string name)
        {
            var album = getBehaviour.Find(a => a.Name == name).FirstOrDefault();
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }
            else
            {
                var imagepathlist = new List<int>(album.Images.Count);
                foreach (var item in album.Images)
                {
                    imagepathlist.Add(item.Id);
                }
                return imagepathlist;
            }
        }
        /// <summary>
        /// Used to create new Empty album in database, creates a new folder by the name of album
        /// </summary>
        /// <param name="AlbumName">Album name or folder name</param>
        /// <returns>id of album</returns>
        public int NewAlbum(string AlbumName,string baseFolder)
        {
            var albumfolder = Directory.CreateDirectory(Path.Combine(baseFolder+ "Images" + AlbumName));
            return postBehaviour.Post(new Models.Album() { FolderPath = albumfolder.FullName, Images = new List<Models.Image>(), Name = AlbumName });
        }
        /// <summary>
        /// Used to create new album in database, creates a new folder by the name of album and moves all images associated to it there.
        /// </summary>
        /// <param name="AlbumName">Album name or folder name</param>
        /// <param name="ListOfImages"> list of paths of images of album</param>
        /// <returns> id of album</returns>
        public int NewAlbum(string AlbumName, IEnumerable<string> ListOfImages, string baseFolder)
        {
            var albumfolder = Directory.CreateDirectory(Path.Combine(baseFolder, "Images" , AlbumName));
            var Album = new Models.Album() { FolderPath = albumfolder.FullName, Images = new List<Models.Image>(), Name = AlbumName };
            foreach (var item in ListOfImages)
            {
                if (!File.Exists(item))
                {
                    var newPathofImage = albumfolder.FullName + "\\" + Path.GetFileName(item);
                    if (newPathofImage != item)
                    {
                        File.Move(item, newPathofImage);
                    }
                    Album.Images.Add(new Models.Image() { Path = newPathofImage, Version = 1 });
                }
            }
            return postBehaviour.Post(Album);
        }
        /// <summary>
        /// Adds an Image to the existing album.
        /// </summary>
        /// <param name="albumid"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public int AddImageToAlbum(int albumid, string imagePath)
        {
            return updateBehaviour.AddImageToTheAlbum(albumid, imagePath);
        }
        public int AddImageToAlbum(int albumid, int imageid)
        {
            return updateBehaviour.AddImageToTheAlbum(albumid, imageid);
        }
        public void DeleteAlbum(int albumid)
        {
            deleteBehaviour.Delete(getBehaviour.Get(albumid));
        }
        public IEnumerable<string> GetAllAlbumNames()
        {
            return getBehaviour.GetAll().Select(a => a.Name).AsEnumerable();
        }
        public IEnumerable<int> GetAll()
        {
            return getBehaviour.GetAll().Select(a => a.Id).AsEnumerable();
        }
    }
}
