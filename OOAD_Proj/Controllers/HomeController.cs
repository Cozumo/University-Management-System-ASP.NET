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
    public class HomeController : Controller
    {
        private DB_proj_OOADEntities db = new DB_proj_OOADEntities();
        // GET: Home
        public ActionResult Index()
        {
            Session["UserName"] = null;
            return View();
        }

        public ActionResult Userview()
        {
            var events = db.Events.Include(S => S.Semester);
            Session["UserName"] = null;
            return View(events.ToList());
        }

        public ActionResult Contact()
        {
            Session["UserName"] = null;
            return View();
        }

        public ActionResult Courses()
        {
            var courses = db.Courses.Include(c => c.Department);
            Session["UserName"] = null;
            return View(courses);
        }

        public ActionResult About()
        {
            Session["UserName"] = null;
            return View();
        }
        public ActionResult Login()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Logins");
            }
            return View();
        }
    }
}