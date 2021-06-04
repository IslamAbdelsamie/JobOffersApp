using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace JobOffersApp.Models
{
    public class CVsTable
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser  { get; set; }

        public string CV  { get; set; }
    }
}