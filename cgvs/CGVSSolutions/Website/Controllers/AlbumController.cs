using log4net;
using Services;
using Services.ServiceComponents.AlbumsTypedBehaviour;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class AlbumController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AlbumController));
        AlbumService albumService = null;

        public AlbumController()
        {
            WebConfigurationManager webConfiguration = WebConfigurationManager.Instance;
            var constr = webConfiguration.ConnectionString;
            albumService = new AlbumService(new WebsiteAlbumGetBehaviour(constr), new WebsiteAlbumPostBehaviour(constr), new WebsiteAlbumUpdateBehaviour(constr), new WebsiteAlbumDeleteBehaviour(constr));

        }
        [HttpGet]
        public JsonResult Get(int id)
        {
            Log.Debug("getting ImageIds associated to albumId " + id);
            return Json(albumService.GetImageIdsAssociatedToAlbum(id).ToArray(), "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            Log.Debug("getting all albumIds.");

            try
            {
                return Json(albumService.GetAll().ToArray(), "application/json", JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                Log.Debug(ex.Message);
                return new JsonResult();
            }
        }

        [HttpGet]
        public JsonResult GetfirstfourImages(int id)
        {
            
            var result = Json(albumService.GetImageIdsAssociatedToAlbum(id).ToList().Take(4).ToArray(), "application/json", JsonRequestBehavior.AllowGet);
            Log.Debug("getting any 4 ImageIds associated to albumId " + id);

            return result;
        }

        [HttpPost]
        public ActionResult Post(AlbumUploadModel album)
        {
            var uploadedFiles = new List<HttpPostedFileBase>(album.Files);

            if (uploadedFiles.Count >= 1)
            {
                Log.Debug(uploadedFiles.Count + "files to be uploaded.");
                List<string> savePaths = new List<string>();
                foreach (var httpPostedFile in uploadedFiles)
                {
                    if (httpPostedFile.ContentLength>=0 && (httpPostedFile.ContentType == "image/jpeg" || httpPostedFile.ContentType == "image/png"))
                    {
                        var filename = httpPostedFile.FileName.Trim();

                        Log.Debug(filename + " file is saving.");
                        if (filename.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                        {
                            continue;
                        }
                        var savePath = Path.Combine(ControllerContext.HttpContext.Server.MapPath("~/Content/Temp"), filename);
                        httpPostedFile.SaveAs(savePath);
                        savePaths.Add(savePath);
                        Log.Debug(filename + " file is saved at " + savePath);
                    }
                }

                var albumpath = ControllerContext.HttpContext.Server.MapPath("~/Content");
                Log.Debug("creating Album folder " + album.AlbumName + " at " + albumpath);
                if (albumService.NewAlbum(album.AlbumName, savePaths, albumpath) != 0)
                {
                    return PartialView("SaveSuccessModal",savePaths.Count);
                }
                else
                    return PartialView("SaveFailureModal", savePaths.Count);
            }
            else
                return PartialView("NoImagesModal");
        }


    }
}
