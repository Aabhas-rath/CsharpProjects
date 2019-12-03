using Services.ServiceComponents.ImageBehaviours;

namespace Services
{
    public class ImageService
    {
        private IImageGetBehaviour ImageGet;
        private IImagePostBehaviour ImagePost;
        private IImageDeleteBehaviour ImageDelete;

        public ImageService(IImageGetBehaviour getBehaviour, IImagePostBehaviour postBehaviour = null, IImageDeleteBehaviour deleteBehaviour = null)
        {
            ImageGet = getBehaviour;
            ImagePost = postBehaviour;
            ImageDelete = deleteBehaviour;
        }
        public string PathOfImage(int id)
        {
            return ImageGet.GetPathOfImage(id);
        }
        public string PathOfImage(int id,int version)
        {
           return ImageGet.GetPathOfImage(id,version);
        }

    }
}
