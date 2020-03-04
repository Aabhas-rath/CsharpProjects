using Repository.Core;
using Repository.Persistance.Repositories;

namespace Repository.Persistance
{
    public class RepositoryWorker : IRepositoryWorker
    {
        private static RepositoryWorker _instance = null;
        private static readonly object lockObj = new object();

        private CGVSContext context = null;
        protected RepositoryWorker()
        {
            context = CGVSContext.context();
            Images = ImageRepository.Instance(context);
            Albums = AlbumRepository.Instance(context);
        }
        protected RepositoryWorker( string DBCS)
        {
            context = CGVSContext.context(DBCS);
            Images = ImageRepository.Instance(context);
            Albums = AlbumRepository.Instance(context);
        }

        public static RepositoryWorker Instance()
        {
            if (_instance == null)
            {
                lock (lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new RepositoryWorker();
                    }
                }
            }
            return _instance;
        }
        public static RepositoryWorker Instance(string DBCS)
        {
            if (_instance == null)
            {
                lock (lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new RepositoryWorker(DBCS);
                    }
                }
            }
            return _instance;
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
