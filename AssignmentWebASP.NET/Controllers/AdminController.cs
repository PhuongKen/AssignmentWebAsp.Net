using AssignmentWebASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentWebASP.NET.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin adminModel)
        {
            using (DbModels db = new DbModels())
            {
                var email = db.Admins.Where(x => x.email == adminModel.email && x.password == adminModel.password).FirstOrDefault();
                if (email == null)
                {
                    adminModel.LoginErrorMessage = "Sai tài khoản hoặc mật khẩu.";
                    return View("Login", adminModel);
                }
                else
                {
                    Session["AdminID"] = email.id;
                    return RedirectToAction("Index", "Admin");
                }

            }
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }

    }
}