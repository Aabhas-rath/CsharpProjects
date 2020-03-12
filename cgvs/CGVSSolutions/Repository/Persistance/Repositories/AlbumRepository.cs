using Models;
using Repository.Core.Repositories;
using System.Linq;

namespace Repository.Persistance.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        private static AlbumRepository _instance = null;
        private static readonly object lockobj = new object();
        protected AlbumRepository(CGVSContext context):base(context)
        {
        }
        public static AlbumRepository GetRepository(CGVSContext con)
        {
            if (_instance == null)
            {
                lock (lockobj)
                {
                    if (_instance == null)
                    {
                        _instance = new AlbumRepository(con);
                    }
                }
            }
            else if (con != _instance.Context)
            {
                lock (lockobj)
                {
                    _instance = new AlbumRepository(con); 
                }
            }
            return _instance;
        }
        public int Count()
        {
            return base.GetAll().Count();
        }

        public string GetFolderPathOfAlbum(int id)
        {
            return base.Get(id).FolderPath;
        }
    }
}
