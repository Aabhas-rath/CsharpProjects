//using Repository.Implementations;
using log4net;
using Services;
using Services.ServiceComponents.ImageTypedBehaviour;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class ImagesController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ImagesController));
        private ImageService _websiteImageService = null;
        public ImagesController()
        {
            WebConfigurationManager webConfiguration = WebConfigurationManager.Instance;

            _websiteImageService = new ImageService(new WebSiteImageGetBehaviour(webConfiguration.ConnectionString));
        }

        [HttpGet]
        public ActionResult GetImage(int id)
        {
            var path = _websiteImageService.PathOfImage(id);
            Log.Debug($"Loading image of id {id} from path {path}.");
            ApplicationEventLogger.LogApplication($"Loading image of id {id} from path {path}.");
            return base.File(path, "image/jpeg|image/png");
        }
       

    }
}