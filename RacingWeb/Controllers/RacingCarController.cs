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
        public async Task<ActionResult> GetCars()
        {
            var listDTOCars = await _racingCarService.GetAllAsync();
            var listViewCars = _mapper.Map<IEnumerable<RacingCarView>>(listDTOCars);
            return Json(new { data = listViewCars }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Save(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await MakeDropDownMenuLists();
            if (id == 0)
            {
                return View(new RacingCarView());
            }
            var racingCarDTO = await _racingCarService.FindByIdAsync(id);
            if (racingCarDTO != null)
            {
                var racingCarView = _mapper.Map<RacingCarView>(racingCarDTO);
                return View(racingCarView);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(RacingCarView racingCar)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var updDTORacingCar = _mapper.Map<RacingCarDTO>(racingCar);
                if (racingCar.Id > 0)
                {
                    //Edit 
                    await _racingCarService.UpdateAsync(updDTORacingCar);
                }
                else
                {
                    //Save
                    await _racingCarService.CreateAsync(updDTORacingCar);
                }
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
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
            bool status = false;
            var deleteResult = await _racingCarService.RemoveAsync(id);
            if (deleteResult)
            {
                status = true;
                return new JsonResult { Data = new { status = status } };
            }
            return new JsonResult { Data = new { status = status } };
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
        [HttpGet]
        public JsonResult CheckModelName(string name)
        {
            return Json(IsModelNameOccuped(name), JsonRequestBehavior.AllowGet);
        }

        private bool IsModelNameOccuped(string modelName)
        {
            var getItemByModel = _racingCarService.FindByModel(modelName);
            return getItemByModel != null ? false : true;
        }
    }
}
