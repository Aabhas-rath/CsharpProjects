using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PictureContent
    {
        public int PicContentId { get; set; }
        public int PicId { get; set; }
        public string Caption { get; set; }
        public string Path { get; set; }
        public string PictureFileName { get; set; }
        public string PictureThumbnailPath { get; set; }
        public string TakenBy { get; set; }
    }
}
