using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobOffersApp.Models;
using System.Data.Entity;
using JobOffersApp.ViewModels;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace JobOffersApp.Controllers
{
    public class JobsController : Controller
    {
        private ApplicationDbContext _context;

        public JobsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Jobs
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            //var JobsList = _context.Jobs.Include(J => J.Categories).Include(J => J.Specialization).Include(J => J.ApplicationUser).ToList();
            var JobsList = _context.Jobs.Select(J => new { J.Categories, J.Specialization, J.ApplicationUser }).ToList();
            return View(JobsList);
        }

        [HttpGet]
        [Authorize(Roles ="Publisher")]
        public ActionResult Create()
        {
            var viewModel = new JobsViewModel
            {
                Categories = _context.Categories.ToList(),
                Specializations = _context.Specializations.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Publisher")]
        public ActionResult Create(Jobs jobs, HttpPostedFileBase Imagefile)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new JobsViewModel
                {
                    Jobs = jobs,
                    Categories = _context.Categories.ToList(),
                    Specializations = _context.Specializations.ToList()
                };

                return View("Create", viewModel);
            } else
            {
                if (Imagefile != null && Imagefile.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(Imagefile.FileName);
                    string extenstion = Path.GetExtension(Imagefile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmsssfff") + extenstion;
                    jobs.JobImage = "~/Uploads/Jobs/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Uploads/Jobs/"), fileName);
                    Imagefile.SaveAs(fileName);

                }

                jobs.ApplicationUserId = User.Identity.GetUserId();
                jobs.PublishDate = DateTime.Now;
                _context.Jobs.Add(jobs);
                _context.SaveChanges();
                TempData["SM"] = "The Job has been added successfully";
                return RedirectToAction("ListJobsByPublisher", "Jobs");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Publisher")]
        public ActionResult Edit(int Id)
        {
            var Job = _context.Jobs.FirstOrDefault(j => j.Id == Id);
            if (Job == null)
            {
                return Content("This Job does not exist");
            } else
            {
                var viewModel = new JobsViewModel
                {
                    Jobs = Job,
                    Categories = _context.Categories.ToList(),
                    Specializations = _context.Specializations.ToList()
                };
                return View(viewModel);
            }

        }

        [HttpPost]
        [Authorize(Roles = "Publisher")]
        public ActionResult Edit(Jobs jobs, HttpPostedFileBase ImageFile)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new JobsViewModel
                {
                    Jobs = jobs,
                    Categories = _context.Categories.ToList(),
                    Specializations = _context.Specializations.ToList()
                };
                return View("Edit", viewModel);
            }
            else
            {
                var JobInDb = _context.Jobs.Single(j => j.Id == jobs.Id);
                JobInDb.JobName = jobs.JobName;
                JobInDb.JobDescription = jobs.JobDescription;
                JobInDb.SpecializationId = jobs.SpecializationId;
                JobInDb.CategoriesId = jobs.CategoriesId;

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var FileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    var Extenstion = Path.GetExtension(ImageFile.FileName);
                    FileName = FileName + DateTime.Now.ToString("yymmsssfff") + Extenstion;
                    JobInDb.JobImage = "~/Uploads/Jobs/" + FileName;
                    FileName = Path.Combine(Server.MapPath("~/Uploads/Jobs/"), FileName);
                    ImageFile.SaveAs(FileName);
                }

                _context.SaveChanges();
                TempData["SM"] = "The Job has been Edited successfully";
                return RedirectToAction("ListJobsByPublisher", "Jobs");
            }

        }

        [Authorize(Roles = "Publisher")]
        public ActionResult Delete(int Id)
        {
            var Job = _context.Jobs.FirstOrDefault(j => j.Id == Id);
            if (Job == null)
            {
                return Content("This Job does not exist");
            } else
            {
                _context.Jobs.Remove(Job);
                _context.SaveChanges();
                return RedirectToAction("ListJobsByPublisher", "Jobs");
            }
        }

        public ActionResult Details(int Id)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Id == Id);
            Session["JobId"] = Id;
            var viewModel = new JobsViewModel
            {
                Jobs = job,
                Categories = _context.Categories.Where(c => c.Id == job.CategoriesId).ToList(),
                Specializations = _context.Specializations.Where(s => s.Id == job.SpecializationId).ToList()
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult ApplyForJobs()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="Job Seeker")]
        public ActionResult ApplyForJobs(ApplyForJobs applyForJobs)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            if (applyForJobs.JobMessage == null)
            {
                ModelState.AddModelError("", "Please Enter Your Message");
                return View("ApplyForJobs", applyForJobs);
            }
            
            else
            {
                var UserId = User.Identity.GetUserId();
                var JobId = (int)Session["JobId"];

                if (_context.ApplyForJobs.Any(j => j.ApplicationUserId == UserId && j.JobsId == JobId))
                {
                    TempData["SM"] = "You already applied to this job before";
                    return View("ApplyForJobs", applyForJobs);
                } else
                {
                    applyForJobs.JobsId = JobId;
                    applyForJobs.ApplicationUserId = UserId;
                    applyForJobs.AppliedDate = DateTime.Now;

                    var CVId = _context.CVsTables.Where(C => C.ApplicationUserId == UserId).SingleOrDefault();
                    applyForJobs.CVsTableId = CVId.Id;

                    _context.ApplyForJobs.Add(applyForJobs);
                    _context.SaveChanges();
                    TempData["SM"] = "You successfully applied for this job";
                    return RedirectToAction("Details", "Jobs", new { Id = JobId });
                }

                
            }
        }

        public ActionResult JobListing()
        {
            var JobsList = _context.Jobs.Include(J => J.Categories).Include(J => J.Specialization).ToList();
            return View(JobsList);
        }

        public ActionResult ListByJobSpecialization(int Id)
        {
            var Jobs = _context.Jobs.Where(j => j.SpecializationId == Id).Include(j => j.Categories).Include(j => j.Specialization).ToList();
            var SpecializationId = _context.Specializations.FirstOrDefault(s => s.Id == Id);
            ViewBag.Title = SpecializationId.SpecializationName;
            return View(Jobs);
        }

        public ActionResult ListByJobCategories(int Id)
        {
            var jobs = _context.Jobs.Where(j => j.CategoriesId == Id).Include(j => j.Categories).Include(j => j.Specialization).ToList();
            var CategoryId = _context.Categories.FirstOrDefault(c => c.Id == Id);
            ViewBag.Title = CategoryId.CategoryName;
            return View(jobs);
        }

        [Authorize(Roles = "Publisher")]
        public ActionResult ListJobsByPublisher()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = _context.Jobs.Where(j => j.ApplicationUserId == UserId).Include(j => j.Categories).Include(j => j.Specialization).ToList();

            return View(Jobs);
        }


        public ActionResult ListJobByApplicant(int Id)
        {
            var AppliedJobList = _context.ApplyForJobs.Where(J => J.JobsId == Id).Include(C => C.CVsTable).ToList();

            var ApplicantViewModel = new JobsApplicantViewModel
            {
                applyForJobs = AppliedJobList,
                applicationUsers = _context.Users.ToList()
            };
            return View(ApplicantViewModel);
        }

        [HttpGet]
        public ActionResult SearchForJobs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchForJobs(string search, int? Specialization, int? Category)
        {
            if (search == "" && Specialization == null && Category == null)
            {
                TempData["SM"] = "You must enter one of the search criteria";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                List<JobOffersApp.Models.Jobs> SearchResults = new List<JobOffersApp.Models.Jobs>();

                if (search != null)
                {
                    if (Specialization != null && Category != null)
                    {
                        SearchResults = _context.Jobs.Where(J => J.JobName.Contains(search) || J.JobDescription.Contains(search)).Where(s => s.SpecializationId == Specialization && s.CategoriesId == Category).Include(c => c.Categories).Include(s => s.Specialization).ToList();
                    }
                    else if (Specialization == null && Category != null)
                    {
                        SearchResults = _context.Jobs.Where(J => J.JobName.Contains(search) || J.JobDescription.Contains(search)).Where(s => s.CategoriesId == Category).Include(c => c.Categories).Include(s => s.Specialization).ToList();
                        return View(SearchResults);
                    }
                    else if (Specialization != null && Category == null)
                    {
                        SearchResults = _context.Jobs.Where(J => J.JobName.Contains(search) || J.JobDescription.Contains(search)).Where(s => s.SpecializationId == Specialization).Include(c => c.Categories).Include(s => s.Specialization).ToList();
                    }
                    else
                    {
                        SearchResults = _context.Jobs.Where(J => J.JobName.Contains(search) || J.JobDescription.Contains(search)).Include(c => c.Categories).Include(s => s.Specialization).ToList();
                    }

                    return View(SearchResults);
                }
                else
                {
                    if (Specialization != null && Category != null)
                    {
                        SearchResults = _context.Jobs.Where(s => s.SpecializationId == Specialization && s.CategoriesId == Category).Include(c => c.Categories).Include(s => s.Specialization).ToList();
                    }
                    else if (Specialization == null && Category != null)
                    {
                        SearchResults = _context.Jobs.Where(s => s.CategoriesId == Category).Include(c => c.Categories).Include(s => s.Specialization).ToList();
                    }
                    else if (Specialization != null && Category == null)
                    {
                        SearchResults = _context.Jobs.Where(s => s.SpecializationId == Specialization).Include(c => c.Categories).Include(s => s.Specialization).ToList();
                    }
                    return View(SearchResults);
                }
            }
        }
    }

}