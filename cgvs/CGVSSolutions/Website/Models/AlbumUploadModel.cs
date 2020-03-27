using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Website.Models
{
    public class AlbumUploadModel
    {
        [Required(ErrorMessage = "Please select Images.")]
        [Display(Name = "Browse Images")]
        public List<HttpPostedFileBase> Files { get; set; }

        [Required(ErrorMessage = "Please give a name to the album")]
        [Display(Name = "Album name")]
        [RegularExpression("[A-Za-z0-9.-]")]
        public string AlbumName { get; set; }

    }
}