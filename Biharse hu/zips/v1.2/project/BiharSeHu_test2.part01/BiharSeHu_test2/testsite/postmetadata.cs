//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace testsite
{
    using System;
    using System.Collections.Generic;
    
    public partial class postmetadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public postmetadata()
        {
            this.PictureMetadatas = new HashSet<PictureMetadata>();
            this.postContents = new HashSet<postContent>();
            this.Tags = new HashSet<Tag>();
        }
    
        public int postId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> noOfViews { get; set; }
        public Nullable<int> noOfLikes { get; set; }
        public Nullable<bool> hasPics { get; set; }
        public Nullable<bool> hasTags { get; set; }
        public Nullable<int> noOfPics { get; set; }
        public Nullable<int> noOfTags { get; set; }
        public Nullable<bool> isSponsoredPost { get; set; }
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
