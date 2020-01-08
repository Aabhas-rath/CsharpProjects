//using Repository.Implementations;
using Services;
using Services.ServiceComponents.ImageTypedBehaviour;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class ImagesController : Controller
    {
        private ImageService _websiteImageService = null;
        public ImagesController()
        {
            WebConfigurationManager webConfiguration = new WebConfigurationManager();

            _websiteImageService = new ImageService(new WebSiteImageGetBehaviour(webConfiguration.ConnectionString));
            //    images = new ImageRepository(webConfiguration.ConnectionString);
        }
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetImage(int id)
        {
            var path = _websiteImageService.PathOfImage(id);
            return base.File(path, "image/jpeg|image/png");
        }
    }
}