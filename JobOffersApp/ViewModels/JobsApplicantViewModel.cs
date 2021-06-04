using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using JobOffersApp.Models;

namespace JobOffersApp.ViewModels
{
    public class JobsApplicantViewModel
    {
        public IEnumerable<ApplyForJobs> applyForJobs  { get; set; }
        public IEnumerable<ApplicationUser> applicationUsers  { get; set; }
    }
}