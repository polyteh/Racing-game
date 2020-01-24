using AutoMapper;
using Ninject;
using RacingDTO.Interfaces;
using RacingDTO.Models;
using RacingDTO.Services;
using RacingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace RacingWeb.Controllers
{
    public class MakeRaceController : Controller
    {
        //как можно сделать readonly?
        private IRaceService _raceService;
        private readonly IMapper _mapper;
        private RaceView newRaceView;
        //to do  inject
        [Inject]
        public MakeRaceController(IRaceService raceService, IMapper mapper)
        {
            _raceService = raceService;
            _mapper = mapper;
            newRaceView = new RaceView();
        }
        // GET: MakeRace
        public ActionResult Index()
        {
            IEnumerable<SimpleCarView> carsToRace = new List<SimpleCarView>();
            carsToRace = TempData["RaceCarList"] as IEnumerable<SimpleCarView>;
            newRaceView.CarList = _mapper.Map<IEnumerable<CarStatusView>>(carsToRace).ToList();
            Session["raceView"] = newRaceView;
            Session["raceService"] = _raceService;
            return View();
        }
        public ActionResult GetStatus()
        {
            _raceService = (IRaceService)Session["raceService"];
            if (!_raceService.isRunning())
            {
                RaceView raceView = (RaceView)Session["raceView"];
                return Json(raceView.CarList, JsonRequestBehavior.AllowGet);
            }
            List<CarStatusDTO> carStatusList = _raceService.GetRaceStatus();

            return Json(_mapper.Map<IEnumerable<CarStatusView>>(carStatusList), JsonRequestBehavior.AllowGet);
        }
        //как правильно сюда получить список из CarListPartial()? можно через TempData, но это костыль
        [HttpGet]
        public void StartRace()
        {
            _raceService = (IRaceService)Session["raceService"];
            var newBLRace = _mapper.Map<RaceDTO>((RaceView)Session["raceView"]);
            _raceService.StartRace(newBLRace);
            Session["raceService"] = _raceService;

        }
        public ActionResult isRaceRunning()
        {
            _raceService = (IRaceService)Session["raceService"];
            return Json(_raceService.isRunning(),JsonRequestBehavior.AllowGet);
        }
    }
}
