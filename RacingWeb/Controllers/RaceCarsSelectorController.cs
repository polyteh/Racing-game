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
            SelectSimpleCarsView listSimpleViewCars = new SelectSimpleCarsView();
            listSimpleViewCars.CarList= _mapper.Map<IEnumerable<SimpleCarForSelectorView>>(listDTOCars).ToList();
            return View(listSimpleViewCars);
        }
        [HttpPost]
        public ActionResult Index(SelectSimpleCarsView selectionCarList)
        {
            //save selection list to transfer to another controller
            SelectSimpleCarsView selectedCarsOnly = new SelectSimpleCarsView();
            List<SimpleCarForSelectorView> selectedCarList = new List<SimpleCarForSelectorView>();
            foreach (var item in selectionCarList.CarList)
            {
                if (item.IsSelected)
                {
                    selectedCarList.Add(item);
                }
            }
            selectedCarsOnly.CarList = selectedCarList;
            TempData["RaceCarList"] = selectedCarsOnly;
            return RedirectToAction("Index", "MakeRace");

        }
    }
}
