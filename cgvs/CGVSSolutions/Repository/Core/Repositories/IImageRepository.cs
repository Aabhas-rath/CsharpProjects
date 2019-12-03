using Models;
using System.Collections.Generic;

namespace Repository.Core.Repositories
{
    public interface IImageRepository : IRepository<Images>
    {
        string GetPathOfImage(int id);
        string GetPathOfImage(int id, int Version);
        Dictionary<int,string> GetPathsOfAllVersionsOfImage(int id); 
    }
}
