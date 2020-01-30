using Models;
using Services.Core.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceComponents.AlbumBehaviours
{
    public interface IAlbumGetBehaviour : IGetBehaviour<Album>
    {
        string GetFolderOfAlbum(int id);
        int GetNumberOfImagesInAlbum(int id);
        IEnumerable<Image> GetAllImagesAssociated(int id);
    }
}
