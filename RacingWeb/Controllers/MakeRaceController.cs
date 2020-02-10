using AutoMapper;
using Newtonsoft.Json;
using Ninject;
using RacingDTO.Interfaces;
using RacingDTO.Models;
using RacingDTO.Services;
using RacingWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RacingWeb.Controllers
{
    public class MakeRaceController : Controller
    {
        //как можно сделать readonly?
        private IRaceService _raceService;
        private readonly IRaceDBDTOService _raceceDBService;
        private readonly IMapper _mapper;
        private RaceView _newRaceView;
        //to do  inject
        [Inject]
        public MakeRaceController(IRaceService raceService, IMapper mapper, IRaceDBDTOService raceDBService)
        {
            _raceService = raceService;
            _mapper = mapper;
            _raceceDBService = raceDBService;
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
            List<CarStatusDTO> carStatusList = new List < CarStatusDTO > (); 
            _raceService.GetRaceStatus();

            string path = @"d:\Education\A-Level\Temp\JSON\raceStatus.json";
            string jsonResults;
            Debug.WriteLine("READ JSON!!!!");
            using (var tw = new StreamReader(path))
            {
                //without ToString also works
                jsonResults= tw.ReadLine();
                tw.Close();
            }
            carStatusList = JsonConvert.DeserializeObject<List<CarStatusDTO>>(jsonResults);

            //string JSONresult = JsonConvert.DeserializeObject< List < CarStatusDTO >>
            //Task task1 = new Task(() =>
            //{
            //    string path = @"d:\Education\A-Level\Temp\JSON\raceStatus.json";
            //    using (var tw = new StreamReader(path))
            //    {
            //        //without ToString also works
            //        tw.WriteLineAsync(JSONresult.ToString());
            //        tw.Close();
            //    }
            //});
            //task1.Start();



            carStatusList.OrderByDescending(x => x.Place);
            return Json(_mapper.Map<IEnumerable<CarStatusView>>(carStatusList), JsonRequestBehavior.AllowGet);
        }
        //синхронный метод.При нем нет возврата в _raceService.StartRace(newBLRace) после завершения гонки
        [HttpGet]
        public void StartRace()
        {
            _newRaceView = (RaceView)Session["raceView"];
            _raceService = (IRaceService)Session["raceService"];
            _newRaceView.isStarted = true;
            var newBLRace = _mapper.Map<RaceDTO>((RaceView)Session["raceView"]);
            Session["raceService"] = _raceService;
            Session["raceView"] = _newRaceView;
            _raceService.StartRace(newBLRace);
        }

        //асинхронный метод.При нем нем блокируется вывод
        //[HttpGet]
        //public async Task StartRace()
        //{
        //    _newRaceView = (RaceView)Session["raceView"];
        //    _raceService = (IRaceService)Session["raceService"];
        //    _newRaceView.isStarted = true;
        //    var newBLRace = _mapper.Map<RaceDTO>((RaceView)Session["raceView"]);
        //    Session["raceService"] = _raceService;
        //    Session["raceView"] = _newRaceView;
        //    await _raceService.StartRace(newBLRace);
        //}

        public void PauseRace()
        {
            _raceService = (IRaceService)Session["raceService"];
            _raceService.PauseRace();
        }

        public void ResumeRace()
        {
            _raceService = (IRaceService)Session["raceService"];
            _raceService.ResumeRace();
        }

        public bool IsRaceRunning()
        {
            _raceService = (IRaceService)Session["raceService"];
            Debug.WriteLine($"Race from controller: {_raceService.IsRunning()}");
            return _raceService.IsRunning();
        }
        public async Task SaveRaceResultToDB()
        {
            _raceService = (IRaceService)Session["raceService"];
            List<CarStatusDTO> carStatusList = _raceService.GetRaceStatus();
            RaceDBDTO savedRace = new RaceDBDTO() { Name=DateTime.Now.ToString(), CarStat= MapCarsStatToDB(carStatusList) };
            await _raceceDBService.CreateAsync(savedRace);
        }
        private List<CarStatDTO> MapCarsStatToDB(List<CarStatusDTO> carStatusFromRace)
        {
            List<CarStatDTO> carStatToDb = new List<CarStatDTO>();
            foreach (var car in carStatusFromRace)
            {
                carStatToDb.Add(new CarStatDTO() { RacingCarId=car.Id, Place=(int)car.Place});
            }
            return carStatToDb;
        }
    }
}
