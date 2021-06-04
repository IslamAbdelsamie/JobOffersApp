using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobOffersApp.Models;

namespace JobOffersApp.ViewModels
{
    public class ListJobsViewModel
    {
        public IEnumerable<Jobs> Jobs  { get; set; }
        public IEnumerable<ApplyForJobs> applyForJobs  { get; set; }
    }
}