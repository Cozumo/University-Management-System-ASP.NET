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
    public class TransDetailsController : Controller
    {
        private DB_proj_OOADEntities db = new DB_proj_OOADEntities();

        // GET: TransDetails
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var transDetails = db.TransDetails.Include(t => t.Student1);
            return View(transDetails.ToList());
        }

        public ActionResult StudentView()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var transDetails = db.TransDetails.Include(t => t.Student1);
            return View(transDetails.ToList());
        }

        // GET: TransDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransDetail transDetail = db.TransDetails.Find(id);
            if (transDetail == null)
            {
                return HttpNotFound();
            }
            return View(transDetail);
        }

        // GET: TransDetails/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Student = new SelectList(db.Students, "S_id", "S_pass");
            return View();
        }

        // POST: TransDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "trans_id,trans_details,trans_date,Student")] TransDetail transDetail)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                db.TransDetails.Add(transDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Student = new SelectList(db.Students, "S_id", "S_pass", transDetail.Student);
            return View(transDetail);
        }

        // GET: TransDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransDetail transDetail = db.TransDetails.Find(id);
            if (transDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Student = new SelectList(db.Students, "S_id", "S_pass", transDetail.Student);
            return View(transDetail);
        }

        // POST: TransDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "trans_id,trans_details,trans_date,Student")] TransDetail transDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Student = new SelectList(db.Students, "S_id", "S_pass", transDetail.Student);
            return View(transDetail);
        }

        // GET: TransDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransDetail transDetail = db.TransDetails.Find(id);
            if (transDetail == null)
            {
                return HttpNotFound();
            }
            return View(transDetail);
        }

        // POST: TransDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransDetail transDetail = db.TransDetails.Find(id);
            db.TransDetails.Remove(transDetail);
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
