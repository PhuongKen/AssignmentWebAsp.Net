using AssignmentWebASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentWebASP.NET.Areas.AdminManager.Controllers
{
    public class LoginController : Controller
    {
        // GET: AdminManager/Login

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(Admin adminModel)
        {
            using (DbModels db = new DbModels())
            {
                var email = db.Admins.Where(x => x.email == adminModel.email && x.password == adminModel.password).FirstOrDefault();
                if (email == null)
                {
                    adminModel.LoginErrorMessage = "Sai tài khoản hoặc mật khẩu.";
                    return View("Index", adminModel);
                }
                else
                {
                    Session["AdminID"] = email.id;
                    return RedirectToAction("/Index","Home");
                }

            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}