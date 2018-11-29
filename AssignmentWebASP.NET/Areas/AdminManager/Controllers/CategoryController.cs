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
    public class CategoryController : Controller
    {
        DbModels db = new DbModels();

        // GET: AdminManager/Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category,HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string ImageName = System.IO.Path.GetFileName(image.FileName);
                    string physicalPath = Server.MapPath("~/Assets/Image/Category/" + ImageName);
                    image.SaveAs(physicalPath);
                    category.image = ImageName;
                    category.createAt = DateTime.Now;
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Category category = db.Categories.Find(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Category category = db.Categories.Find(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string ImageName = System.IO.Path.GetFileName(image.FileName);
                    string physicalPath = Server.MapPath("~/Assets/Image/Category/" + ImageName);
                    image.SaveAs(physicalPath);
                    category.image = ImageName;
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(category);
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
            Category category = db.Categories.Find(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Category ca)
        {
            try
            {
                Category category = new Category();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    category = db.Categories.Find(id);
                    if (category == null)
                        return HttpNotFound();
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch
            {
                return View();
            }
        }
    }
}