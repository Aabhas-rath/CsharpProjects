using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using System.Threading.Tasks;
using Models.Interfaces;

namespace Repository.Interfaces
{
    interface IImageRepository:ICrudRepository<Images>
    {
        string GetImageOfVersion(int id, int version);
        List<Images> GetAllImagesOfId(int id);
        string GetPathOfImage(int id);
    }
}
