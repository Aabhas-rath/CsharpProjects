using Models;
using Services.Core.Behaviours;

namespace Services.ServiceComponents.AlbumBehaviours
{
    public interface IAlbumUpdateBehaviour :IUpdateBehaviour<Album>
    {
        int AddImageToTheAlbum(int AlbumId, int ImageId);
        int AddImageToTheAlbum(int AlbumId, string ImagePath);
        int DeleteImageFromAlbum(int AlbumId, int ImageId);
    }
}
