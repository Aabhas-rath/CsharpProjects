using Repository.Core;
using Repository.Persistance.Repositories;
using Repository.Users;

namespace Repository.Persistance
{
    public class RepositoryWorker : IRepositoryWorker
    {

        private CGVSContext context = null;
        private ImageRepository images;
        private AlbumRepository albums;
        private ApplicationUser users;

        public RepositoryWorker()
        {
            context = new CGVSContext();
            init();
        }
        public RepositoryWorker(string DBCS)
        {
            context = new CGVSContext(DBCS);
            init();
        }
        private void init()
        {
            images = new ImageRepository(context);
            albums = new AlbumRepository(context);
        }
        public ImageRepository Images
        {
            get
            {
                if (images == null)
                {
                    images = new ImageRepository(context);
                }
                return images;
            }
            private set => images = value;
        }
        public AlbumRepository Albums
        {
            get
            {
                if (albums == null)
                {
                    albums = new AlbumRepository(context);
                }
                return albums;
            }
            private set => albums = value;
        }
        
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
