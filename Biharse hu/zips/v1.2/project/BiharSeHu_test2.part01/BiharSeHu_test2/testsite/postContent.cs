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
    
    public partial class postContent
    {
        public int postContentId { get; set; }
        public int postId { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
    
        public virtual postmetadata postmetadata { get; set; }
    }
}
