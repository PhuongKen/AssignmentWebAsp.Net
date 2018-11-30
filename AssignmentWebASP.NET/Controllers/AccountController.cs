﻿using AssignmentWebASP.NET.Models;
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
                else
                {
                    var pass = customerModel.password;
                    var MD5Pass = Encryptor.MD5Hash(pass);
                    customerModel.password = MD5Pass;
                    customerModel.ConfirmPassword = MD5Pass;
                    customerModel.createAt = DateTime.Now;
                    customerModel.status = 0;
                    dbModel.Customers.Add(customerModel);
                    dbModel.SaveChanges();
                }
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
                var pass = customerModel.password;
                var MD5Pass = Encryptor.MD5Hash(pass);
                var email = db.Customers.Where(x => x.email == customerModel.email && x.password == MD5Pass).FirstOrDefault();
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