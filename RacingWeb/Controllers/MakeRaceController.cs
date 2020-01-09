using RacingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RacingWeb.Controllers
{
    public class MakeRaceController : Controller
    {
        // GET: MakeRace
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult CarListPartial()
        {
            SelectSimpleCarsView carsToRace = new SelectSimpleCarsView();
            carsToRace = TempData["RaceCarList"] as SelectSimpleCarsView;
            return PartialView(carsToRace);
        }
    }
}
