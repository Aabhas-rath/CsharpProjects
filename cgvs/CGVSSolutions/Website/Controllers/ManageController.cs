using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class ManageController : Controller
    {
        AlbumController albumController = null;

        public ManageController()
        {
            albumController = new AlbumController();
            
        }
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadFiles()
        {
            AlbumUploadModel Albums = new AlbumUploadModel();
            return View(Albums);
        }
        [HttpPost]
        public ActionResult UploadFiles(AlbumUploadModel Album)
        {
           
            var ctrctx = ControllerContext;
            albumController.ControllerContext = this.ControllerContext;
            return albumController.Post(Album);
        }
    }
}