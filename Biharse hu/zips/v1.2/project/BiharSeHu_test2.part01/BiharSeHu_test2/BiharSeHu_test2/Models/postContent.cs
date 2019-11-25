namespace BiharSeHu_test2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("postContent")]
    public partial class postContent
    {
        public int postContentId { get; set; }

        public int postId { get; set; }

        [StringLength(500)]
        public string Heading { get; set; }

        public string Content { get; set; }

        public virtual postmetadata postmetadata { get; set; }
    }
}
