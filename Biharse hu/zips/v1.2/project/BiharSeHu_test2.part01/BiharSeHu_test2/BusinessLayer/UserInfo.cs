using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserInfo
    {
        public int userid { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int NoOfPosts { get; set; }
        public int NoOfTags { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool isAuthor { get; set; }
        public int AdminId { get; set; }
        public bool isAdmin { get; set; }
    }
}
