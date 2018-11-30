using AssignmentWebASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AssignmentWebASP.NET.Areas.AdminManager.Controllers
{
    public class ProductController : Controller
    {
        // GET: AdminManager/Product
        DbModels db = new DbModels();

        // GET: AdminManager/product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string ImageName = System.IO.Path.GetFileName(image.FileName);
                    string physicalPath = Server.MapPath("~/Assets/Image/Product/" + ImageName);
                    image.SaveAs(physicalPath);
                    product.image = ImageName;
                    product.createAt = DateTime.Now;
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }


        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var categoryQuyryString = from d in db.Categories
                                   orderby d.name
                                   select d;
            ViewBag.categoryId = new SelectList(categoryQuyryString, "id", "name", selectedDepartment);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Product product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Product product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string ImageName = System.IO.Path.GetFileName(image.FileName);
                    string physicalPath = Server.MapPath("~/Assets/Image/Product/" + ImageName);
                    image.SaveAs(physicalPath);
                    product.image = ImageName;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Product product = db.Products.Find(id);
            if (product == null)
                HttpNotFound();
            return View(product);
        }

        // POST: product/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Product pr)
        {
            try
            {
                Product product = new Product();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    product = db.Products.Find(id);
                    if (product == null)
                        return HttpNotFound();
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }
    }
}