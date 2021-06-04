using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobOffersApp.Models;

namespace JobOffersApp.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;

        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }
        
        // GET: Admin/Categories
        public ActionResult Index()
        {
            var CategoriesList = _context.Categories.ToList();
            return View(CategoriesList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                categories.CategorySlug = categories.CategoryName.Replace(" ", "").ToLower();
                var Check = _context.Categories.FirstOrDefault(c => c.CategorySlug == categories.CategorySlug);
                if (Check != null)
                {
                    ModelState.AddModelError("", "This category already exists");
                    return View("Create", categories);
                }
                else
                {
                    _context.Categories.Add(categories);
                    _context.SaveChanges();
                    TempData["SM"] = "The Category has been added successfully";

                    return RedirectToAction("Index", "Categories");
                }
                

            }
            else
            {
                return View("Create", categories);
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Category = _context.Categories.FirstOrDefault(c => c.Id == Id);
            if (Category == null)
            {
                return Content("This category does not exist");
            } else
            {
                return View(Category);
            }
        }

        [HttpPost]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                categories.CategorySlug = categories.CategoryName.Replace(" ", "").ToLower();
                var check = _context.Categories.FirstOrDefault(c => c.CategorySlug == categories.CategorySlug);
                if (check != null)
                {
                    ModelState.AddModelError("", "This category already exist");
                    return View("Edit", categories);
                }
                else
                {
                    var CategoryInDb = _context.Categories.FirstOrDefault(c => c.Id == categories.Id);
                    CategoryInDb.CategoryName = categories.CategoryName;
                    CategoryInDb.CategorySlug = categories.CategorySlug;
                    _context.SaveChanges();
                    TempData["SM"] = "The Category has been edited successfully";
                    return RedirectToAction("Index", "Categories");
                }

                
            } else
            {
                return View("Edit", categories);
            }

        }

        public ActionResult Delete(int Id)
        {
            var Category = _context.Categories.FirstOrDefault(c => c.Id == Id);
            if (Category == null)
            {
                return Content("This category does not exist");
            }
            else
            {
                _context.Categories.Remove(Category);
                _context.SaveChanges();
                TempData["SM"] = "The Category has been deleted successfully";
                return RedirectToAction("Index", "Categories");
            }
        }

    }
}