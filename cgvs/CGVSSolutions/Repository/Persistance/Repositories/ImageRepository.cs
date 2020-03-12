using Models;
using Repository.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Persistance.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        private static ImageRepository _instance = null;
        private static readonly object lockobj = new object();


        protected ImageRepository(CGVSContext context) : base(context)
        {
        }

        public static ImageRepository GetRepository(CGVSContext context) 
        {
            if (_instance == null)
            {
                lock (lockobj)
                {
                    if (_instance == null)
                    {
                        _instance = new ImageRepository(context);
                    }
                }
            }
            else if (context != _instance.Context)
            {
                lock (lockobj)
                {
                    _instance = new ImageRepository(context);
                }
            }
            return _instance;
        }


        public string GetPathOfImage(int id)
        {
            return base.Find(i => (i.Id == id)).OrderByDescending(i => i.Version).First().Path; ;
        }

        public string GetPathOfImage(int id, int Version)
        {
            return base.Find(i => ((i.Id == id) && (i.Version == Version))).First().Path; 
        }

        public Dictionary<int,string> GetPathsOfAllVersionsOfImage(int id)
        {
            var versions = base.Find(i => (i.Id == id)).OrderByDescending(i => i.Version);
            var returnObject = new Dictionary<int,string>(versions.Count());
            foreach (var item in versions)
            {
                returnObject.Add(item.Version, item.Path);
            }
            return returnObject;
        }
    }
}
