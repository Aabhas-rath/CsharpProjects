using Services;
using Services.ServiceComponents.AlbumsTypedBehaviour;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
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
            var contentPath = ControllerContext.HttpContext.Server.MapPath("~");
            var TempFolderPath = Path.Combine(contentPath, "Content", "Temp");
            if (uploadedFiles.Count >= 1)
            {
                List<string> savePaths = new List<string>();
                var tempFolder = Directory.CreateDirectory(TempFolderPath);
                tempFolder.EnumerateFiles().ToList().ForEach(f => f.Delete());
                foreach (var httpPostedFile in uploadedFiles)
                {
                    if (httpPostedFile.ContentLength>=0 && (httpPostedFile.ContentType == "image/jpeg" || httpPostedFile.ContentType == "image/png"))
                    {
                        var filename = httpPostedFile.FileName.Trim();
                        if (filename.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                        {
                            continue;
                        }
                        var savePath = Path.Combine(tempFolder.FullName, filename);
                        httpPostedFile.SaveAs(savePath);
                        savePaths.Add(savePath);
                    }
                }
                
                if (albumService.NewAlbum(album.AlbumName, savePaths, contentPath) != 0)
                {
                    return PartialView("SaveSuccessModal", album);
                }
                else
                    return PartialView("SaveFailureModal", album);
            }
            else
                return PartialView("NoImagesModal");
        }
    }
}
