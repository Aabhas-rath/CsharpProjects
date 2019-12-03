using Models;
using Models.Interfaces;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class ImageRepository : CRUDRepository<Images>, IImageRepository
    {
        public ImageRepository(string connectionString) : base(connectionString)
        {
        }

        public List<Images> GetAllImagesOfId(int id)
        {
            return base.Find(m => (m.Id == id)).ToList();
        }

        public string GetImageOfVersion(int id, int version)
        {
            return GetAllImagesOfId(id).Where(m => (m.Version == version)).FirstOrDefault().Path;
        }

        public string GetPathOfImage(int id)
        {
            return base.Get(id).Path;
        }
    }
}
