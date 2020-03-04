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
            return Json(albumService.GetImageIdsAssociatedToAlbum(id).ToArray(),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return Json(albumService.GetAll().ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetfirstfourImages(int id)
        {
            var result = Json(albumService.GetImageIdsAssociatedToAlbum(id).ToList().Take(4).ToArray(), JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public ActionResult Post(AlbumUploadModel album)
        {
            var uploadedFiles = new List<HttpPostedFileBase>(album.Files);

            if (uploadedFiles.Count >= 1)
            {
                List<string> savePaths = new List<string>();
                foreach (var httpPostedFile in uploadedFiles)
                {
                    if (httpPostedFile.ContentLength>=0 && (httpPostedFile.ContentType == "image/jpeg" || httpPostedFile.ContentType == "image/png"))
                    {
                        var filename = httpPostedFile.FileName.Trim();
                        if (filename.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                        {
                            continue;
                        }
                        var ctrctx = ControllerContext;
                        var savePath = Path.Combine(ControllerContext.HttpContext.Server.MapPath("~/Content/Temp"), filename);
                        httpPostedFile.SaveAs(savePath);
                        savePaths.Add(savePath);
                    }
                }
                if (albumService.NewAlbum(album.AlbumName, savePaths, ControllerContext.HttpContext.Server.MapPath("~/Content")) != 0)
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
