using AutoMapper;
using RacingDTO.Interfaces;
using RacingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RacingWeb.Controllers
{
    public class ResultController : Controller
    {
        private readonly IRaceDBDTOService _raceceDBService;
        private readonly IMapper _mapper;
        public ResultController(IRaceDBDTOService raceDBService, IMapper mapper)
        {
            _mapper = mapper;
            _raceceDBService = raceDBService;
        }
        // GET: Result
        public ActionResult Index()
        {
            var alldata = _raceceDBService.GetAll();
            var dataToShow = alldata.SelectMany(race => race.CarStat, (race, car) =>
                new
                {
                    Racename = race.Name,
                    CarName = car.RacingCar.Name,
                    car.Place
                }
                ).ToList();
            return View();
        }
        [HttpGet]
        public string GetResults()
        {
            var alldata = _raceceDBService.GetAll();
            var dataToShow = alldata.SelectMany(race => race.CarStat, (race, car) =>
                new
                {
                    Racename = race.Name,
                    CarName = car.RacingCar.Name,
                    car.Place
                }
                ).ToList();
            System.Web.Script.Serialization.JavaScriptSerializer jSearializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return jSearializer.Serialize(dataToShow);
        }
    }
}