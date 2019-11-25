namespace BiharSeHu_test2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("postmetadata")]
    public partial class postmetadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public postmetadata()
        {
            PictureMetadatas = new HashSet<PictureMetadata>();
            postContents = new HashSet<postContent>();
            Tags = new HashSet<Tag>();
        }

        [Key]
        public int postId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedOn { get; set; }

        public int? noOfViews { get; set; }

        public int? noOfLikes { get; set; }

        public bool? hasPics { get; set; }

        public bool? hasTags { get; set; }

        public int? noOfPics { get; set; }

        public int? noOfTags { get; set; }

        public bool? isSponsoredPost { get; set; }

        public int authorId { get; set; }

        public virtual LoginInfo LoginInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PictureMetadata> PictureMetadatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<postContent> postContents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
