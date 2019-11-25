using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PostMetaData
    {
        public int PostId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int NoOfViews { get; set; }
        public int NoOfLikes { get; set; }
        public bool HasPics { get; set; }
        public bool HasTags { get; set; }
        public int NoOfPics { get; set; }
        public int NoOfTags { get; set; }
        public bool IsSponsoredPost { get; set; }
        public int AssociatedAuthorID { get; set; }
    }
}
