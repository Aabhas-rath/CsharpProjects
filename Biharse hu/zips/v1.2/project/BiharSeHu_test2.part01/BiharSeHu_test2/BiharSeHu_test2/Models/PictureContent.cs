namespace BiharSeHu_test2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PictureContent")]
    public partial class PictureContent
    {
        [Key]
        public int PicContentId { get; set; }

        public int? PicId { get; set; }

        [StringLength(200)]
        public string Caption { get; set; }

        [StringLength(50)]
        public string Path { get; set; }

        [StringLength(20)]
        public string PictureFileName { get; set; }

        [StringLength(50)]
        public string PicThumbnailPath { get; set; }

        [StringLength(30)]
        public string takenBy { get; set; }

        public virtual PictureMetadata PictureMetadata { get; set; }
    }
}
