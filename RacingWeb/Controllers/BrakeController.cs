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
    public class BrakeController : Controller
    {
        private readonly IBrakeService _brakeService;
        private readonly IMapper _mapper;
        [Inject]
        public BrakeController(IBrakeService brakeService, IMapper mapper)
        {
            _brakeService = brakeService;
            _mapper = mapper;
        }
        // GET: Brake
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> GetBrakes()
        {
            var listDTOBrakes = await _brakeService.GetAllAsync();
            var listViewBrakes = _mapper.Map<IEnumerable<BrakeView>>(listDTOBrakes);
            return Json(new { data = listViewBrakes }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Save(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == 0)
            {
                return View(new BrakeView());
            }
            var brakeDTO = await _brakeService.FindByIdAsync(id);
            if (brakeDTO != null)
            {
                var brakeView = _mapper.Map<BrakeView>(brakeDTO);
                return View(brakeView);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(BrakeView brake)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var updDTOBrake = _mapper.Map<BrakeDTO>(brake);
                if (brake.Id > 0)
                {
                    //Edit 
                    await _brakeService.UpdateAsync(updDTOBrake);
                }
                else
                {
                    //Save
                    await _brakeService.CreateAsync(updDTOBrake);
                }
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brakeToDeleteDTO = await _brakeService.FindByIdAsync(id);
            if (brakeToDeleteDTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var brakeToDeleteView = _mapper.Map<BrakeView>(brakeToDeleteDTO);
            return View(brakeToDeleteView);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            bool status = false;
            var deleteResult = await _brakeService.RemoveAsync(id);
            if (deleteResult)
            {
                status = true;
                return new JsonResult { Data = new { status = status } };
            }
            return new JsonResult { Data = new { status = status } };
        }


        [HttpGet]
        public JsonResult CheckModelName(string name)
        {
            return Json(IsModelNameOccuped(name), JsonRequestBehavior.AllowGet);
        }

        private bool IsModelNameOccuped(string modelName)
        {
            var getItemByModel = _brakeService.FindByModel(modelName);
            return getItemByModel != null ? false : true;
        }
    }
}

