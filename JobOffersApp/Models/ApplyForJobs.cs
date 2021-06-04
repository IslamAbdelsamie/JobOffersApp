using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JobOffersApp.Models
{
    public class ApplyForJobs
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Message")]
        [MaxLength(50)]
        public string JobMessage  { get; set; }

        public int JobsId { get; set; }
        public Jobs Jobs  { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser  { get; set; }

        public DateTime AppliedDate  { get; set; }

        public int CVsTableId { get; set; }
        public CVsTable CVsTable  { get; set; }
    }
}