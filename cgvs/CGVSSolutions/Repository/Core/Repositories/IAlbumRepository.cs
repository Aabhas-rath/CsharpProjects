using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core.Repositories
{
    public interface IAlbumRepository :IRepository<Album>
    {
        string GetFolderPathOfAlbum(int id);
        int Count();
    }
}
