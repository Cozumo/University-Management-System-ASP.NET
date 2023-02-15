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
    public class StudentsController : Controller
    {
        private DB_proj_OOADEntities db = new DB_proj_OOADEntities();

        // GET: Students
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var students = db.Students.Include(s => s.Department).Include(s => s.Semester);
            return View(students.ToList());
        }
        public ActionResult Studentinfo(int? id)
        {

            if (Session["Username"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                id = Convert.ToInt32(Session["Username"]);
            }
            Student Students = db.Students.Find(id);
            if (Students == null)
            {
                return HttpNotFound();
            }
            return View(Students);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.S_depart = new SelectList(db.Departments, "D_id", "D_name");
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "S_id,S_pass,S_name,S_fees,S_cgpa,S_semester,S_major,S_depart")] Student student)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.S_depart = new SelectList(db.Departments, "D_id", "D_name", student.S_depart);
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name", student.S_semester);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.S_depart = new SelectList(db.Departments, "D_id", "D_name", student.S_depart);
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name", student.S_semester);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "S_id,S_pass,S_name,S_fees,S_cgpa,S_semester,S_major,S_depart")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.S_depart = new SelectList(db.Departments, "D_id", "D_name", student.S_depart);
            ViewBag.S_semester = new SelectList(db.Semesters, "Semester_id", "Semester_name", student.S_semester);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
