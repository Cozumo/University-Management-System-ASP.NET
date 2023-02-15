using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OOAD_Proj.Models;

namespace OOAD_Proj.Controllers
{
    public class ClassSchedulesController : Controller
    {
        private DB_proj_OOADEntities db = new DB_proj_OOADEntities();

        // GET: ClassSchedules
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var classSchedules = db.ClassSchedules.Include(c => c.Course1).Include(c => c.Semester);
            return View(classSchedules.ToList());
        }

        public ActionResult StudentView()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var classSchedules = db.ClassSchedules.Include(c => c.Course1).Include(c => c.Semester);
            return View(classSchedules.ToList());
        }

        // GET: ClassSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            if (classSchedule == null)
            {
                return HttpNotFound();
            }
            return View(classSchedule);
        }

        // GET: ClassSchedules/Create
        public ActionResult Create()
        {
            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name");
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name");
            return View();
        }

        // POST: ClassSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clsScd,Course,scd_date,S_semester")] ClassSchedule classSchedule)
        {
            if (ModelState.IsValid)
            {
                db.ClassSchedules.Add(classSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name", classSchedule.Course);
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name", classSchedule.S_semester);
            return View(classSchedule);
        }

        // GET: ClassSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            if (classSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name", classSchedule.Course);
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name", classSchedule.S_semester);
            return View(classSchedule);
        }

        // POST: ClassSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clsScd,Course,scd_date,S_semester")] ClassSchedule classSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name", classSchedule.Course);
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name", classSchedule.S_semester);
            return View(classSchedule);
        }

        // GET: ClassSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            if (classSchedule == null)
            {
                return HttpNotFound();
            }
            return View(classSchedule);
        }

        // POST: ClassSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            db.ClassSchedules.Remove(classSchedule);
            db.SaveChanges();
            return RedirectToAction("Index");
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
