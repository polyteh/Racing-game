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
        public async Task<ActionResult> Index()
        {
            var listDTOEngine = await _brakeService.GetAllAsync();
            var listViewEngine = _mapper.Map<IEnumerable<BrakeView>>(listDTOEngine);
            return View(listViewEngine);
        }

        // GET: Brake/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Brake/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brake/Create
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(BrakeView newBrake)
        {
            if (ModelState.IsValid)
            {
                var newBLBrake = _mapper.Map<BrakeDTO>(newBrake);
                await _brakeService.CreateAsync(newBLBrake);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Not Valid";
                return View(newBrake);
            }
        }

        // GET: Brake/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brakeDTO = await _brakeService.FindByIdAsync(id);
            if (brakeDTO!=null)
            {
                var brakeView = _mapper.Map<BrakeView>(brakeDTO);
                return View(brakeView);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        // POST: Brake/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(BrakeView editBrake)
        {
            if (ModelState.IsValid)
            {
                var newBLBrake = _mapper.Map<BrakeDTO>(editBrake);
                await _brakeService.UpdateAsync(newBLBrake);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Not Valid";
                return View(editBrake);
            }
        }
        [Authorize]
        // GET: Brake/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brakesToDeleteDTO = await _brakeService.FindByIdAsync(id);
            if (brakesToDeleteDTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var brakeToDeleteView = _mapper.Map<BrakeView>(brakesToDeleteDTO);
            return View(brakeToDeleteView);
        }

        // POST: Brake/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            var deleteResult = await _brakeService.RemoveAsync(id);
            if (deleteResult)
            {
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
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

