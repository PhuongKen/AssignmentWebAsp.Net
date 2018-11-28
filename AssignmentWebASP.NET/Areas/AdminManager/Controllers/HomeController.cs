using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentWebASP.NET.Areas.AdminManager.Controllers
{
    public class HomeController : Controller
    {
        // GET: AdminManager/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}