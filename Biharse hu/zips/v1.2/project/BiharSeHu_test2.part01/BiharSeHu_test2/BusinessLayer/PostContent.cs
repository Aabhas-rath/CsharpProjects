using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PostContent
    {
        public int PostContentId { get; set; }
        public int PostId { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
    }
}
