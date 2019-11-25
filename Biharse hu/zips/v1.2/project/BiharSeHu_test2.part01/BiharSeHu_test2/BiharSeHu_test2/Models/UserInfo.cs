namespace BiharSeHu_test2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserInfo")]
    public partial class UserInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInfo()
        {
            Tags = new HashSet<Tag>();
        }

        [Key]
        public int userId { get; set; }
        [Display(Name ="Added on")]
        [Column(TypeName = "date")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name ="Full Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [StringLength(15)]
        [Display(Name ="Also Known As")]
        [DataType(DataType.Text)]
        public string DisplayName { get; set; }

        public int NoOfPosts { get; set; }

        public int NoOfTags { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public bool isAuthor { get; set; }

        public int AdminId { get; set; }

        public bool isAdmin { get; set; }

        public virtual LoginInfo LoginInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
