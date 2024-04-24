using Job_websit.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if (job == null)
            {
                return HttpNotFound();
            }
            //sava id in Action details and can use in session 
            Session["JobId"] = JobId;
            return View(job);
        }

        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Apply(Job Cv, HttpPostedFileBase uploadCv)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];

            var Chek = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            var job = new ApplyForJob();
            if (Chek.Count < 1)
            {
                job.JobId = JobId;
                job.UserId = UserId;

                job.ApplyDate = DateTime.Now;
                string path = Path.Combine(Server.MapPath("~/UploadsCv"), uploadCv.FileName);
                uploadCv.SaveAs(path);
                job.Cv = uploadCv.FileName;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "تمت التقدم الى الوضيفة بنجاح";
            }
            else
            {
                ViewBag.Result = "لا يمكنك التقديم مرتين على نفس الوضيفة";
            }
            return View();

        }

        [Authorize(Roles = "Deler")]
        public ActionResult GetAllUser()
        {
            var db = new ApplicationDbContext();
     
            var CurrentUser = db.Users;

            return View(CurrentUser.ToList());
        }



        [Authorize]
        public ActionResult Getjob()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }

        [Authorize]
        public ActionResult Detailsofjob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            //sava id in Action details and can use in session 

            return View(job);
        }


        [Authorize]
        // GET: Roles/Edit/5
        public ActionResult Editofjob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();

            }
            return View(job);
        }

        // POST: Roles/Edit/5
     
        public ActionResult Editofjob(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }






        [Authorize]
        // GET: Roles/Delete/5
        public ActionResult Deleteofjob(int id)
        {

            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();

            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Deleteofjob(ApplyForJob job)
        {

            var myjob = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(myjob);
            db.SaveChanges();

            return RedirectToAction("Index");


        }

       [Authorize]
        public ActionResult GetJobsbyPublisher()
        {
            var UserID = User.Identity.GetUserId();
            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserID
                       select app;


            //var grouped = from j in Jobs
            //              group j by j.job.JopTitle
            //              into gr
            //              select new JobsViewModel
            //              {
            //                  JobTitle = gr.Key,
            //                  Items = gr
            //              };
            return View(Jobs.ToList());
        }


        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(a => a.JopTitle.Contains(searchName)
            || a.JobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();
            return View(result);
        }

    }
}