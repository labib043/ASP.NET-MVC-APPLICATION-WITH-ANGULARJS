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
using System.IO;

namespace EmployeeMVCAngualrJS.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(t => t.Gender);
            return View(employees.ToList());
        }

        //[HttpGet]
        //public ActionResult GetEmployee()
        //{
        //    return Json(GetData(), JsonRequestBehavior.AllowGet);
        //}
        public JsonResult getAllData(Employee employee)
        {
            var info = db.Employees.ToList().Join(
                 db.Genders.ToList(), p => p.GenderId, c => c.Id, (pl, cl) => new Employee
                 {
                     Id = pl.Id,
                     GenderId = cl.Id,
                     GName = cl.Name,
                     IsActive = pl.IsActive,
                     JoiningDate = pl.JoiningDate,
                     Name = pl.Name,

                 }).ToList();
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", employee.GenderId);
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return new ContentResult { Content = "Succesfull" };
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", employee.GenderId);
            return new ContentResult { Content = "Unsuccesfull" };
        }

        [HttpPost]
        public ActionResult UpdateEmployee(Employee employee)
        {
            if(ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return new ContentResult { Content = "Updated" };
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", employee.GenderId);
            return new ContentResult { Content = "Failed" };
        }

        [HttpPost]
        public ContentResult Delete(Employee employee)
        {
            try
            {
                Employee employeeDel = db.Employees.Find(employee.Id);
                db.Employees.Remove(employeeDel);
                db.SaveChanges();
                return new ContentResult { Content = "Success!Your data is deleted" };
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
