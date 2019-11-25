using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PictureMetadata
    {
        public int PicId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int FileSize { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Format { get; set; }
        public int PicturePriority { get; set; }
        public bool IsPostBackground { get; set; }
        public bool IsPostMainPicture { get; set; }
        public int AssociatedPostId { get; set; }
    }
}
