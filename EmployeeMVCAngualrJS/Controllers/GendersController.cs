using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeMVCAngualrJS.Data;
using EmployeeMVCAngualrJS.Models;
using Newtonsoft.Json;

namespace EmployeeMVCAngualrJS.Controllers
{
    public class GendersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Genders.ToList());
        }

        
        public JsonResult getAllData()
        {
            List<Gender> genders = new List<Gender>();



            genders = db.Genders.ToList().Select(s => new Gender
            {
                Name = s.Name,
                Id = s.Id,
                Employees = null
            }).ToList();
            return Json(db.Genders.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ContentResult AddGender(Gender gender)
        {
            if (ModelState.IsValid)
            {
                db.Genders.Add(gender);
                db.SaveChanges();
                return new ContentResult
                {
                    Content = "your data is saved"
                };
            }

            return new ContentResult
            {
                Content = "your data  is not saved"
            };
        }
    

        [HttpPost]
        public ContentResult UpdateGender(Gender gender)
        {
           if (ModelState.IsValid)
            {
                db.Entry(gender).State = EntityState.Modified;
                db.SaveChanges();
                return new ContentResult { Content = "Updated" };
            }
            return new ContentResult { Content = "Failed" };
        }
        

        [HttpPost]
        public ActionResult DeleteGender(Gender gender)
        {
            try
            {
                Gender genderDel = db.Genders.Find(gender.Id);
                db.Genders.Remove(genderDel);
                db.SaveChanges();
                return new ContentResult { Content = "deleted" };
            }
            catch
            {

            }

            return new ContentResult { Content = "Fail!Your data is not deleted" };
        }
      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
