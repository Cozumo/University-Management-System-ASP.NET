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
    public class ExamSchedulesController : Controller
    {
        private DB_proj_OOADEntities db = new DB_proj_OOADEntities();

        // GET: ExamSchedules
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var examSchedules = db.ExamSchedules.Include(e => e.Course1).Include(e => e.Semester);
            return View(examSchedules.ToList());
        }

        public ActionResult StudentView()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var examSchedules = db.ExamSchedules.Include(e => e.Course1).Include(e => e.Semester);
            return View(examSchedules.ToList());
        }

        // GET: ExamSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSchedule examSchedule = db.ExamSchedules.Find(id);
            if (examSchedule == null)
            {
                return HttpNotFound();
            }
            return View(examSchedule);
        }

        // GET: ExamSchedules/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name");
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name");
            return View();
        }

        // POST: ExamSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ex_Scd_id,Ex_Sem,Ex_courses,S_semester,Course")] ExamSchedule examSchedule)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                db.ExamSchedules.Add(examSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name", examSchedule.Course);
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name", examSchedule.S_semester);
            return View(examSchedule);
        }

        // GET: ExamSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSchedule examSchedule = db.ExamSchedules.Find(id);
            if (examSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name", examSchedule.Course);
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name", examSchedule.S_semester);
            return View(examSchedule);
        }

        // POST: ExamSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ex_Scd_id,Ex_Sem,Ex_courses,S_semester,Course")] ExamSchedule examSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name", examSchedule.Course);
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name", examSchedule.S_semester);
            return View(examSchedule);
        }

        // GET: ExamSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSchedule examSchedule = db.ExamSchedules.Find(id);
            if (examSchedule == null)
            {
                return HttpNotFound();
            }
            return View(examSchedule);
        }

        // POST: ExamSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamSchedule examSchedule = db.ExamSchedules.Find(id);
            db.ExamSchedules.Remove(examSchedule);
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
