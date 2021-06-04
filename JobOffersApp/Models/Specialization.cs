using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JobOffersApp.Models
{
    public class Specialization
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Specialization Name")]
        [MaxLength(20)]
        public string SpecializationName  { get; set; }

        [MaxLength(20)]
        public string SpecializationSlug  { get; set; }

        [Display(Name = "Specialization Image")]
        public string SpecializationImage  { get; set; }
    }
}