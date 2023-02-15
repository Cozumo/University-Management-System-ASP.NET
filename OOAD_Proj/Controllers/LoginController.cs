using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOAD_Proj.Models;

namespace OOAD_Proj.Controllers
{
    public class LoginController : Controller
    {
        private DB_proj_OOADEntities db = new DB_proj_OOADEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllLog()
        {
            return View();
        }

        public ActionResult TLog(Teacher lg)
        {
            if (ModelState.IsValid == true)
            {
                var credential = db.Teachers.Where(model => model.T_id == lg.T_id && model.T_pass == lg.T_pass).FirstOrDefault();

                if (credential == null)
                { 
                    ViewBag.Errormsg = "Login Failed!"; 
                    return View(); 
                }
                else
                {
                    Session["UserName"] = lg.T_id;
                    return RedirectToAction("TechDash", "Dashboard");
                }
            }
            return View();
        }

        public ActionResult StaffLog(Staff lg)
        {
            if (ModelState.IsValid == true)
            {
                var credential = db.Staffs.Where(model => model.staff_id == lg.staff_id && model.staff_pass == lg.staff_pass).FirstOrDefault();

                if (credential == null)
                {
                    ViewBag.Errormsg = "Login Failed!";
                    return View();
                }
                else
                {
                    Session["UserName"] = lg.staff_id;
                    return RedirectToAction("AdminDash", "Dashboard");
                }
            }
            return View();
        }
        public ActionResult SLog(Student lg)
        {
            if (ModelState.IsValid == true)
            {
                var credential = db.Students.Where(model => model.S_id == lg.S_id && model.S_pass == lg.S_pass).FirstOrDefault();

                if (credential == null)
                {
                    ViewBag.Errormsg = "Login Failed!";
                    return View();
                }
                else
                {
                    Session["UserName"] = lg.S_id;
                    return RedirectToAction("UserDash", "Dashboard");
                }
            }
            return View();
        }
    }
}