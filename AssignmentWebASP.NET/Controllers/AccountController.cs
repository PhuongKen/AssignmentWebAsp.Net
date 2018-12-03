using AssignmentWebASP.NET.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AssignmentWebASP.NET.Controllers
{
    public class AccountController : Controller
    {
        private static List<string> list = new List<string>();
        DbModels db = new DbModels();
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

        [HttpGet]
        public ActionResult update()
        {
            return View();
        }

        public ActionResult UploadImage()
        {
            return View();
        }

        
        public ActionResult uploade()
        {
            bool isSavedSuccessfully = true;
            string fname = "";
            try
            {
                using (DbModels dbModel = new DbModels())
                {
                    foreach (string filename in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[filename];
                        fname = file.FileName;
                        if (file != null && file.ContentLength > 0)
                        {
                            var path = Path.Combine(Server.MapPath("~/Assets/Image/Account/"));
                            string pathstring = Path.Combine(path.ToString());
                            string filename1 = Guid.NewGuid() + Path.GetExtension(file.FileName);

                            bool isexist = Directory.Exists(pathstring);
                            if (!isexist)
                            {
                                Directory.CreateDirectory(pathstring);
                            }
                            string uploadpath = string.Format("{0}\\{1}", pathstring, filename1);
                            list.Add(filename1);
                            file.SaveAs(uploadpath);
                        }
                    }
                    
                    Session["image"] = list;
                }

            }
            catch (Exception)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new
                {
                    Message = fname
                });
            }
            else
            {
                return Json(new
                {
                    Message = "Error in Saving file"
                });
            }
        }

        public ActionResult AddToCart()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var productQueryString = from d in db.Products
                                      orderby d.name
                                      select d;
            var materialQueryString = from d in db.Materials
                                      orderby d.name
                                      select d;
            var sizeQueryString = from d in db.Sizes
                                      orderby d.name
                                      select d;
            ViewBag.productId = new SelectList(productQueryString, "id", "name", selectedDepartment);
            ViewBag.materialId = new SelectList(materialQueryString, "id", "name", selectedDepartment);
            ViewBag.sizeId = new SelectList(sizeQueryString, "id", "name", selectedDepartment);
        }
    }
}