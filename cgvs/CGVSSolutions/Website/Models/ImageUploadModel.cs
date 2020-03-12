using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class ImageUploadModel
    {
        public HttpPostedFile Image { get; set; }
        public string AlbumName { get; set; } = "WebStore";

    }
}