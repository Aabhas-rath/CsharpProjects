namespace BiharSeHu_test2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tag()
        {
            UserInfoes = new HashSet<UserInfo>();
        }

        public int tagId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedOn { get; set; }

        public bool? isNewTag { get; set; }

        public int? tagCounter { get; set; }

        [StringLength(25)]
        public string Content { get; set; }

        public virtual postmetadata postmetadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}
