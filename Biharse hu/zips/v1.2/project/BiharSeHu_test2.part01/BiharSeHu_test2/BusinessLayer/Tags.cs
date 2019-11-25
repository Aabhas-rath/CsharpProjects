using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Tags
    {
        public int TagId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsNewTag { get; set; }
        public int TagCounter { get; set; }
        public string Content { get; set; }
    }
}
