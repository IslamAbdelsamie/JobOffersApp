using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobOffersApp.ViewModels;
using JobOffersApp.Models;

namespace JobOffersApp.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Categories = _context.Categories.ToList(),
                Jobs = _context.Jobs.ToList(),
                Specializations = _context.Specializations.ToList()
            };
            ViewBag.Title = "Home";
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}