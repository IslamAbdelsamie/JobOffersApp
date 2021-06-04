using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JobOffersApp.Models
{
    public class Jobs
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Name")]
        [MaxLength(20)]
        public string JobName  { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(50)]
        public string JobDescription  { get; set; }

        [Display(Name ="Image")]
        public string JobImage  { get; set; }

        [Display(Name = "Category")]
        public int CategoriesId { get; set; }
        public Categories Categories  { get; set; }

        [Display(Name = "Specialization")]
        public int SpecializationId { get; set; }
        public Specialization Specialization  { get; set; }

        [Display(Name ="Publisher Name")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser  { get; set; }


        [Display(Name ="Publish Date")]
        public DateTime PublishDate  { get; set; }
    }
}