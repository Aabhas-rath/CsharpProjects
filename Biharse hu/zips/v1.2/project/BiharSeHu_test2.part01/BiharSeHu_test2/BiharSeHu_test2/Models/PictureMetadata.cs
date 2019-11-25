namespace BiharSeHu_test2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PictureMetadata")]
    public partial class PictureMetadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PictureMetadata()
        {
            PictureContents = new HashSet<PictureContent>();
        }

        [Key]
        public int PicId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedOn { get; set; }

        public int? fileSize { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        [StringLength(8)]
        public string Format { get; set; }

        public int? picturePriority { get; set; }

        public bool? isPostBackground { get; set; }

        public bool? isPostMainPicture { get; set; }

        public int? AssociatedPostId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PictureContent> PictureContents { get; set; }

        public virtual postmetadata postmetadata { get; set; }
    }
}
