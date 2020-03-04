using Models;
using Repository.Core.Repositories;
using System.Linq;

namespace Repository.Persistance.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        private static AlbumRepository _instance;
        private static readonly object padlock = new object();

        public static AlbumRepository Instance(CGVSContext context)
        {
            lock (padlock)
            {
                if (_instance == null)
                {
                    _instance = new AlbumRepository(context);
                }
                return _instance;
            }
            
        }
        protected AlbumRepository(CGVSContext context) : base(context) { }
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
