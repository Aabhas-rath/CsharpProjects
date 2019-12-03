using Models;
using Services.Core.Behaviours;
using System.Collections.Generic;

namespace Services.ServiceComponents.ImageBehaviours
{
    public interface IImageGetBehaviour :IGetBehaviour<Images>
    {
        string GetPathOfImage(int id);
        string GetPathOfImage(int id, int Version);
        Dictionary<int, string> GetPathsOfAllVersionsOfImage(int id);
    }
}
