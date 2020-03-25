using Repository.Core;
using Repository.Persistance.Repositories;

namespace Repository.Persistance
{
    public class RepositoryWorker : IRepositoryWorker
    {

        private CGVSContext context = null;
        public RepositoryWorker()
        {
            context = new CGVSContext();
            Images = new ImageRepository(context);
            Albums = new AlbumRepository(context);
        }
        public RepositoryWorker( string DBCS)
        {
            context = new CGVSContext(DBCS);
            Images = new ImageRepository(context);
            Albums = new AlbumRepository(context);
        }

        public ImageRepository Images { get; private set; }
        public AlbumRepository Albums { get; set; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
