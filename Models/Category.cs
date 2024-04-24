using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Job_websit.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "اسم الصنف ")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "وصف النوع")]
        public string CategoryDescription { get; set; }

    public virtual ICollection<Job> Jpbs { get; set; }
    }
}