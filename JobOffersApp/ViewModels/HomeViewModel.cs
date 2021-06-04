using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobOffersApp.Models;

namespace JobOffersApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Categories> Categories  { get; set; }
        public IEnumerable<Specialization> Specializations { get; set; }
        public IEnumerable<Jobs> Jobs  { get; set; }
    }
}