using AutoMapper;
using Ninject;
using RacingDTO.Interfaces;
using RacingDTO.Models;
using RacingDTO.Services;
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
        private readonly IRaceService _raceService;
        private readonly IMapper _mapper;
        [Inject]
        public MakeRaceController(IRaceService raceService, IMapper mapper)
        {
            _raceService = raceService;
            _mapper = mapper;
        }
        // GET: MakeRace
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult CarListPartial()
        {
            IEnumerable<SimpleCarView> carsToRace = new List<SimpleCarView>();
            carsToRace = TempData["RaceCarList"] as IEnumerable<SimpleCarView>;
            TempData.Keep("RaceCarList");
            return PartialView(carsToRace);
        }
        //как правильно сюда получить список из CarListPartial()? можно через TempData, но это костыль
        [HttpGet]
        public void StartRace()
        {
            RaceView newRaceView = new RaceView();
            newRaceView.CarList= TempData["RaceCarList"] as List<SimpleCarView>;
            var newBLRace = _mapper.Map<RaceDTO>(newRaceView);
            _raceService.StartRace(newBLRace);
        }
    }
}
