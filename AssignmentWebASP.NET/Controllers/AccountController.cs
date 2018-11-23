using AssignmentWebASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentWebASP.NET.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            Customer customerModel = new Customer();
            return View(customerModel);
        }

        [HttpPost]
        public ActionResult Register(Customer customerModel)
        {
            using (DbModels dbModel = new DbModels())
            {
                if (dbModel.Customers.Any(x => x.email == customerModel.email))
                {
                    ViewBag.DuplicateMessage = "Tài khoản này đã tồn tại";
                    return View("Register");
                }
                customerModel.createAt = DateTime.Now;
                dbModel.Customers.Add(customerModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Đăng ký thành công";
            return View("Register", new Customer());
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer customerModel)
        {
            using (DbModels db = new DbModels())
            {
                var email = db.Customers.Where(x => x.email == customerModel.email && x.password == customerModel.password).FirstOrDefault();
                if (email == null)
                {
                    customerModel.LoginErrorMessage = "Sai tài khoản hoặc mật khẩu.";
                    return View("Login", customerModel);
                }
                else
                {
                    Session["id"] = email.id;
                    return RedirectToAction("Index", "Home");
                }

            }
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}