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
    public class AssingedCoursesController : Controller
    {
        private DB_proj_OOADEntities db = new DB_proj_OOADEntities();

        // GET: AssingedCourses
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var assingedCourses = db.AssingedCourses.Include(a => a.Course1).Include(a => a.Student1).Include(a => a.Teacher1);
            return View(assingedCourses.ToList());
        }

        public ActionResult TeacherView()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var assingedCourses = db.AssingedCourses.Include(a => a.Course1).Include(a => a.Teacher1);
            return View(assingedCourses.ToList());
        }

        public ActionResult StudentView()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var assingedCourses = db.AssingedCourses.Include(a => a.Course1).Include(a => a.Student1);
            return View(assingedCourses.ToList());
        }

        // GET: AssingedCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssingedCourse assingedCourse = db.AssingedCourses.Find(id);
            if (assingedCourse == null)
            {
                return HttpNotFound();
            }
            return View(assingedCourse);
        }

        // GET: AssingedCourses/Create
        public ActionResult Create()
        {
            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name");
            ViewBag.Student = new SelectList(db.Students, "S_id", "S_pass");
            ViewBag.Teacher = new SelectList(db.Teachers, "T_id", "T_pass");
            return View();
        }

        // POST: AssingedCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ACourses_id,Acourse_name,Course,Teacher,Student")] AssingedCourse assingedCourse)
        {
            if (ModelState.IsValid)
            {
                db.AssingedCourses.Add(assingedCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name", assingedCourse.Course);
            ViewBag.Student = new SelectList(db.Students, "S_id", "S_pass", assingedCourse.Student);
            ViewBag.Teacher = new SelectList(db.Teachers, "T_id", "T_pass", assingedCourse.Teacher);
            return View(assingedCourse);
        }

        // GET: AssingedCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssingedCourse assingedCourse = db.AssingedCourses.Find(id);
            if (assingedCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name", assingedCourse.Course);
            ViewBag.Student = new SelectList(db.Students, "S_id", "S_pass", assingedCourse.Student);
            ViewBag.Teacher = new SelectList(db.Teachers, "T_id", "T_pass", assingedCourse.Teacher);
            return View(assingedCourse);
        }

        // POST: AssingedCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ACourses_id,Acourse_name,Course,Teacher,Student")] AssingedCourse assingedCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assingedCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Course = new SelectList(db.Courses, "course_id", "course_name", assingedCourse.Course);
            ViewBag.Student = new SelectList(db.Students, "S_id", "S_pass", assingedCourse.Student);
            ViewBag.Teacher = new SelectList(db.Teachers, "T_id", "T_pass", assingedCourse.Teacher);
            return View(assingedCourse);
        }

        // GET: AssingedCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssingedCourse assingedCourse = db.AssingedCourses.Find(id);
            if (assingedCourse == null)
            {
                return HttpNotFound();
            }
            return View(assingedCourse);
        }

        // POST: AssingedCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssingedCourse assingedCourse = db.AssingedCourses.Find(id);
            db.AssingedCourses.Remove(assingedCourse);
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
