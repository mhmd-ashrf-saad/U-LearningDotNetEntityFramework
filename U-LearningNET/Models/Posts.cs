//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace U_LearningNET.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Posts
    {
        public int post_id { get; set; }
        public Nullable<int> course_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string content { get; set; }
        public string file_url { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
    
        public virtual Courses Courses { get; set; }
        public virtual Users Users { get; set; }
    }
}
