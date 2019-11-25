using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Post
    {
        public PostMetaData postMetaData { get; set; }
        public PostContent postContent { get; set; }
        public Post(PostContent pc,PostMetaData pmd)
        {
            this.postMetaData = pmd;
            this.postContent = pc;
        }
        public int AssociatedAuthor { get { return postMetaData.AssociatedAuthorID; } }
    }
}
