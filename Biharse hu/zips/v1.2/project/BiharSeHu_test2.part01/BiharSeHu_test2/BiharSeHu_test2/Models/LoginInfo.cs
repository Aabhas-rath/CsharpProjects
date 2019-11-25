namespace BiharSeHu_test2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoginInfo")]
    public partial class LoginInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoginInfo()
        {
            postmetadatas = new HashSet<postmetadata>();
            UserInfoes = new HashSet<UserInfo>();
        }

        [Key]
        public int AdminId { get; set; }

        [Required]
        //[Display(Name = "Username")]
        [StringLength(15)]
        public string username { get; set; }

        [Required]
        //[Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(15)]
        public string password { get; set; }

        //[Display(Name = "Email id")]
        [DataType(DataType.EmailAddress)]
        [StringLength(30)]
        public string emailId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<postmetadata> postmetadatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}
