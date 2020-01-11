using AutoMapper;
using Ninject;
using RacingDTO.Interfaces;
using RacingDTO.Models;
using RacingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public BrakeController(IBrakeService engineService, IMapper mapper)
        {
            _brakeService = engineService;
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
        public async Task<ActionResult> Edit(int id)
        {
            var brakeDTO = await _brakeService.FindByIdAsync(id);
            var brakeView = _mapper.Map<BrakeView>(brakeDTO);
            return View(brakeView);
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

        // GET: Brake/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Brake/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
