using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RacingWeb.Controllers
{
    public class ComponentsController : Controller
    {
        // GET: Components
        public ActionResult Index()
        {
            ViewBag.ImagePath = @"~/App_Data/Images/Engine.png";
            return View();
        }
    }
}