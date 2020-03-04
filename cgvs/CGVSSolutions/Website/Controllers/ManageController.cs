using log4net;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    [HandleError]
    public class ManageController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ManageController));
        AlbumController albumController = null;

        public ManageController()
        {
            albumController = new AlbumController();
            ApplicationEventLogger.LogApplication("Manage controller loaded");
        }
        [HttpGet]
        public ActionResult UploadFiles()
        {
            AlbumUploadModel Albums = new AlbumUploadModel();
            Log.Debug("AlbumManager loaded");
            ApplicationEventLogger.LogApplication("AlbumManager loaded");
            return PartialView("UploadFiles",Albums);
        }
        [HttpPost]
        public ActionResult UploadFiles(AlbumUploadModel Album)
        {
            var ctrctx = ControllerContext;
            albumController.ControllerContext = this.ControllerContext;
            Log.Debug($"Album {Album.AlbumName} creation started with {Album.Files.Count} files");
            ApplicationEventLogger.LogApplication($"Album {Album.AlbumName} creation started with {Album.Files.Count} files");
            return albumController.Post(Album);
        }
    }
}