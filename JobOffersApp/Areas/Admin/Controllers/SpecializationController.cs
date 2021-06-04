using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobOffersApp.Models;
using System.IO;

namespace JobOffersApp.Areas.Admin.Controllers
{
  
    [Authorize(Roles ="Admin")]
    public class SpecializationController : Controller
    {
        private ApplicationDbContext _context;

        public SpecializationController()
        {
            _context = new ApplicationDbContext();
        }



        // GET: Admin/Specialization
        public ActionResult Index()
        {
            var SpecializationList = _context.Specializations.ToList();
            return View(SpecializationList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Specialization specialization, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                specialization.SpecializationSlug = specialization.SpecializationName.Replace(" ", "").ToLower();
                var check = _context.Specializations.FirstOrDefault(s => s.SpecializationSlug == specialization.SpecializationSlug);
                if (check != null)
                {
                    ModelState.AddModelError("", "This Specialization exists before");
                    return View("Create", specialization);
                }
                else
                {
                    if (ImageFile != null && ImageFile.ContentLength >0)
                    {
                        string FileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                        string Extenstion = Path.GetExtension(ImageFile.FileName);
                        FileName = FileName + DateTime.Now.ToString("yymmsssfff") + Extenstion;
                        specialization.SpecializationImage = "~/Uploads/Specialization/" + FileName;
                        FileName = Path.Combine(Server.MapPath("~/Uploads/Specialization/"), FileName);
                        ImageFile.SaveAs(FileName);
                    }

                    _context.Specializations.Add(specialization);
                    _context.SaveChanges();
                    TempData["SM"] = "The Specialization has been added successfully";
                    return RedirectToAction("Index", "Specialization");
                }

            }
            else
            {
                return View("Create", specialization);
            }

        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Specialization = _context.Specializations.FirstOrDefault(s => s.Id == Id);
            if (Specialization == null)
            {
                return Content("This Specialization does not exist");
            } else
            {
                return View(Specialization);
            }

        }

        [HttpPost]
        public ActionResult Edit(Specialization specialization , HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                specialization.SpecializationSlug = specialization.SpecializationName.Replace(" ", "").ToLower();
                var check = _context.Specializations.Where(s => s.Id != specialization.Id).FirstOrDefault(s => s.SpecializationSlug == specialization.SpecializationSlug);
                if (check != null)
                {
                    ModelState.AddModelError("", "This Specialization exists before");
                    return View("Edit", specialization);
                }
                else
                {
                    var SpecializationInDb = _context.Specializations.FirstOrDefault(s => s.Id == specialization.Id);
                    SpecializationInDb.SpecializationName = specialization.SpecializationName;
                    SpecializationInDb.SpecializationSlug = specialization.SpecializationSlug;

                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                        string extenstion = Path.GetExtension(ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmsssfff") + extenstion;
                        SpecializationInDb.SpecializationImage = "~/Uploads/Specialization/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/Specialization/"), fileName);
                        ImageFile.SaveAs(fileName);
                    }

                    _context.SaveChanges();
                    TempData["SM"] = "The specialization has been edited successfully";
                    return RedirectToAction("Index", "Specialization");
                }
            }
            else
            {
                return View("Edit", specialization);
            }

        }


        public ActionResult Delete(int Id)
        {
            var Specialization = _context.Specializations.FirstOrDefault(s => s.Id == Id);
            if (Specialization == null)
            {
                return Content("This Specialization does not exist");
            }
            else
            {
                _context.Specializations.Remove(Specialization);
                _context.SaveChanges();
                TempData["SM"] = "The Specialization has been deleted successfully";
                return RedirectToAction("Index", "Specialization");
            }
        }
    }
}