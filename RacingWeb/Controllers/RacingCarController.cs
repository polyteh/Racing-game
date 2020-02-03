using AutoMapper;
using Ninject;
using RacingDTO.Interfaces;
using RacingDTO.Models;
using RacingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RacingWeb.Controllers
{
    //create, edit and delete recing cars; cars managment
    public class RacingCarController : Controller
    {
        private readonly IRacingCarService _racingCarService;
        private readonly IEngineService _engineService;
        private readonly IBrakeService _brakeService;
        private readonly ISuspentionService _suspentionService;
        private readonly IMapper _mapper;
        [Inject]
        public RacingCarController(IRacingCarService racingCarService, IEngineService engineService, IBrakeService brakeService,
            ISuspentionService suspentionService, IMapper mapper)
        {
            _racingCarService = racingCarService;
            _engineService = engineService;
            _brakeService = brakeService;
            _suspentionService = suspentionService;
            _mapper = mapper;
        }
        // GET: RacingCar
        public async Task<ActionResult> Index()
        {
            var listDTOCars = await _racingCarService.GetAllAsync();
            var listViewCars = _mapper.Map<IEnumerable<RacingCarView>>(listDTOCars);
            return View(listViewCars);
        }

        // GET: RacingCar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RacingCar/Create
        public async Task<ActionResult> Create()
        {
            await MakeDropDownMenuLists();
            return View();
        }

        // POST: RacingCar/Create
        [HttpPost]
        public async Task<ActionResult> Create(RacingCarView newCar)
        {
            if (ModelState.IsValid)
            {
                var newBLCar = _mapper.Map<RacingCarDTO>(newCar);
                await _racingCarService.CreateAsync(newBLCar);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Not Valid";
                return View(newCar);
            }
        }

        // GET: RacingCar/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var carDTO = await _racingCarService.FindByIdAsync(id);
            if (carDTO != null)
            {
                await MakeDropDownMenuLists();
                var carView = _mapper.Map<RacingCarView>(carDTO);
                return View(carView);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        // POST: RacingCar/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(RacingCarView editCar)
        {
            if (ModelState.IsValid)
            {
                var editBLCar = _mapper.Map<RacingCarDTO>(editCar);
                await _racingCarService.UpdateAsync(editBLCar);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Not Valid";
                return View(editCar);
            }
        }

        // GET: RacingCar/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var racingCarToDeleteDTO = await _racingCarService.FindByIdAsync(id);
            if (racingCarToDeleteDTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var racingCarToDeleteView = _mapper.Map<RacingCarView>(racingCarToDeleteDTO);
            return View(racingCarToDeleteView);

        }

        // POST: RacingCar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, FormCollection collection)
        {
            var deleteResult = await _racingCarService.RemoveAsync(id);
            if (deleteResult)
            {
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
        //как блин, это работает (SelectList)???
        //данные для DropList
        private async Task MakeDropDownMenuLists()
        {
            var listDTOEngine = await _engineService.GetAllAsync();
            var listViewEngine = _mapper.Map<IEnumerable<EngineView>>(listDTOEngine);
            SelectList engines = new SelectList(listViewEngine, "Id", "Name");
            ViewBag.Engines = engines;

            var listDTOBrakes = await _brakeService.GetAllAsync();
            var listViewBrakes = _mapper.Map<IEnumerable<BrakeView>>(listDTOBrakes);
            SelectList brakes = new SelectList(listViewBrakes, "Id", "Name");
            ViewBag.Brakes = brakes;

            var listDTOSuspentions = await _suspentionService.GetAllAsync();
            var listViewSuspentions = _mapper.Map<IEnumerable<SuspentionView>>(listDTOSuspentions);
            SelectList suspentions = new SelectList(listViewSuspentions, "Id", "Name");
            ViewBag.Suspention = suspentions;
        }
    }
}
