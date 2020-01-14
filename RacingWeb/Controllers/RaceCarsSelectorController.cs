using AutoMapper;
using Ninject;
using RacingDTO.Interfaces;
using RacingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RacingWeb.Controllers
{
    //small controller to choose cars for racing
    public class RaceCarsSelectorController : Controller
    {
        private readonly IRacingCarService _racingCarService;
        private readonly IMapper _mapper;
        [Inject]
        public RaceCarsSelectorController(IRacingCarService racingCarService, IMapper mapper)
        {
            _racingCarService = racingCarService;
            _mapper = mapper;
        }
        // GET: RaceCarsSelector
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var listDTOCars = await _racingCarService.GetAllAsync();    
            var simpleCarList= _mapper.Map<IEnumerable<SimpleCarView>>(listDTOCars).ToList();
            return View(simpleCarList);
        }
        [HttpPost]
        public ActionResult Index(IEnumerable<SimpleCarView> selectionCarList)
        {
            //save selection list to transfer to another controller
            List<SimpleCarView> selectedCarsOnly = new List<SimpleCarView>();
            foreach (var item in selectionCarList)
            {
                if (item.IsSelected)
                {
                    selectedCarsOnly.Add(item);
                }
            }
            TempData["RaceCarList"] = selectedCarsOnly;
            return RedirectToAction("Index", "MakeRace");
        }
    }
}
