using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JobOffersApp.Models
{
    public class Categories
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Category Name")]
        [MaxLength(20)]
        public string CategoryName  { get; set; }

        [MaxLength(20)]
        public string CategorySlug { get; set; }
    }
}