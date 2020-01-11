using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RacingWeb.Controllers
{
    //controller to choose action between make race and view results
    public class RacingController : Controller
    {
        // GET: Racing
        public ActionResult Index()
        {
            return View();
        }
    }
}