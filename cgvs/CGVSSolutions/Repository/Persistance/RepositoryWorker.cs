using Repository.Core;
using Repository.Persistance.Repositories;

namespace Repository.Persistance
{
    public class RepositoryWorker : IRepositoryWorker
    {

        private CGVSContext context = null;
        private ImageRepository images;
        private AlbumRepository albums;

        public RepositoryWorker()
        {
            context = new CGVSContext();

        }
        public RepositoryWorker(string DBCS)
        {
            context = new CGVSContext(DBCS);
        }

        public ImageRepository Images { get => images ?? (images = new ImageRepository(context)); private set => images = value; }
        public AlbumRepository Albums { get => albums ?? (albums = new AlbumRepository(context)); private set => albums = value; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            Images = null;
            Albums = null;
        }
    }
}
