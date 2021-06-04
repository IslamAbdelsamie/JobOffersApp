using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobOffersApp.Models;
using JobOffersApp.ViewModels;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.IO;

namespace JobOffersApp.Controllers
{
    
    [Authorize(Roles ="Job Seeker")]
    public class JobSeekerController : Controller
    {
        private ApplicationDbContext _context;

        public JobSeekerController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult ListJobs()
        {
            var UserId = User.Identity.GetUserId();
            var UserJobs = _context.ApplyForJobs.Where(j => j.ApplicationUserId == UserId).ToList();
            var JobList = _context.Jobs.Include(J => J.Categories).Include(S => S.Specialization).ToList();

            var AppliedJobs = from J in JobList
                              join U in UserJobs on J.Id equals U.JobsId
                              select J;

            var AppliedJobViewModel = new ListJobsViewModel
            {
                Jobs = AppliedJobs,
                applyForJobs = UserJobs
            };

            return View(AppliedJobViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var ApplyJobId = _context.ApplyForJobs.FirstOrDefault(J => J.Id == Id);

            return View(ApplyJobId);
        }

        [HttpPost]
        public ActionResult Edit(ApplyForJobs applyForJobs)
        {
            if (!ModelState.IsValid)
            {
                return View(applyForJobs);
            }
            else
            {
                var AppliedJobInDb = _context.ApplyForJobs.First(J => J.Id == applyForJobs.Id);
                AppliedJobInDb.JobMessage = applyForJobs.JobMessage;

                _context.SaveChanges();
                TempData["SM"] = "The Job has been edited successfully";
                return RedirectToAction("ListJobs", "JobSeeker");
            }
        }

        public ActionResult Delete (int Id)
        {
            var AppliedJob = _context.ApplyForJobs.First(J => J.Id == Id);
            _context.ApplyForJobs.Remove(AppliedJob);
            _context.SaveChanges();
            TempData["SM"] = "The Job has been deleted successfully";

            return RedirectToAction("ListJobs", "JobSeeker");
        }

        [HttpGet]
        public ActionResult UploadCV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadCV(CVsTable cVs, HttpPostedFileBase CVfile)
        {
            string fileName = Path.GetFileNameWithoutExtension(CVfile.FileName);
            string extension = Path.GetExtension(CVfile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmsssfff") + extension;
            cVs.CV = "~/Uploads/CVs/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Uploads/CVs/"), fileName);
            CVfile.SaveAs(fileName);

            cVs.ApplicationUserId = User.Identity.GetUserId();
            _context.CVsTables.Add(cVs);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult EditCV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditCV(CVsTable cVs, HttpPostedFileBase CVfile)
        {
            var UserId = User.Identity.GetUserId();
            var UserInDb = _context.CVsTables.Where(u => u.ApplicationUserId == UserId).SingleOrDefault();

           string fileName = Path.GetFileNameWithoutExtension(CVfile.FileName);
            string extension = Path.GetExtension(CVfile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmsssfff") + extension;
            cVs.CV = "~/Uploads/CVs/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Uploads/CVs/"), fileName);
            CVfile.SaveAs(fileName);

            if (UserInDb == null)
            {
                cVs.ApplicationUserId = UserId;
                _context.CVsTables.Add(cVs);
                _context.SaveChanges();

                TempData["SM"] = "The CV has been edited successfully";

                return View("EditCV");
            }
            else
            {
                UserInDb.CV = cVs.CV;
                _context.SaveChanges();

                TempData["SM"] = "The CV has been edited successfully";

                return View("EditCV");
            }
            
        }
    }
}