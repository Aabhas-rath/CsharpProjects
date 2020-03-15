using Models;
using Repository.Core.Repositories;
using System.Linq;

namespace Repository.Persistance.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(CGVSContext context):base(context)
        {
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
