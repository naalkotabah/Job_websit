using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Job_websit.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "اسم الوضيفة")]
        public string JopTitle { get; set; }
        [Display(Name = "وصف الوضيفة")]
        [Required]

        public string JobContent { get; set; }  

     
        [Display(Name = "صورة الوضيفة")]
        public string JopImage { get; set; }


        [Display(Name = "نوع الوضيفة")]
        public int CategoryId { get; set; }
        public string UserID { get; set; }

        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}