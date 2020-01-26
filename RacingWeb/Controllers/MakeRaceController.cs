using AutoMapper;
using Ninject;
using RacingDTO.Interfaces;
using RacingDTO.Models;
using RacingDTO.Services;
using RacingWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private RaceView _newRaceView;
        //to do  inject
        [Inject]
        public MakeRaceController(IRaceService raceService, IMapper mapper)
        {
            _raceService = raceService;
            _mapper = mapper;
            _newRaceView = new RaceView();
        }
        // GET: MakeRace
        public ActionResult Index()
        {
            IEnumerable<SimpleCarView> carsToRace = new List<SimpleCarView>();
            carsToRace = TempData["RaceCarList"] as IEnumerable<SimpleCarView>;
            _newRaceView.CarList = _mapper.Map<IEnumerable<CarStatusView>>(carsToRace).ToList();
            Session["raceView"] = _newRaceView;
            Session["raceService"] = _raceService;
            return View();
        }
        public ActionResult GetStatus()
        {
            _newRaceView = (RaceView)Session["raceView"];
            _raceService = (IRaceService)Session["raceService"];
            if (!_newRaceView.isStarted)
            {
                RaceView raceView = (RaceView)Session["raceView"];
                return Json(raceView.CarList, JsonRequestBehavior.AllowGet);
            }
            List<CarStatusDTO> carStatusList = _raceService.GetRaceStatus();

            return Json(_mapper.Map<IEnumerable<CarStatusView>>(carStatusList), JsonRequestBehavior.AllowGet);
        }
        //синхронный метод. При нем нем возврата в _raceService.StartRace(newBLRace) после завершения гонки
        [HttpGet]
        public void StartRace()
        {
            _newRaceView =(RaceView) Session["raceView"];
            _raceService = (IRaceService)Session["raceService"];
            _newRaceView.isStarted = true;
            var newBLRace = _mapper.Map<RaceDTO>((RaceView)Session["raceView"]);
            Session["raceService"] = _raceService;
            Session["raceView"] = _newRaceView;
            _raceService.StartRace(newBLRace);
        }




        //public ActionResult isRaceRunning()
        //{
        //    _raceService = (IRaceService)Session["raceService"];
        //    return Json(_raceService.isRunning(),JsonRequestBehavior.AllowGet);
        //}

        public bool isRaceRunning()
        {
            _raceService = (IRaceService)Session["raceService"];
            Debug.WriteLine($"Race from controller: {_raceService.isRunning()}");
            return _raceService.isRunning();
        }


    }
}
