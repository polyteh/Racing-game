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
    public class EngineController : Controller
    {
        private readonly IEngineService _engineService;
        private readonly IMapper _mapper;
        [Inject]
        public EngineController(IEngineService engineService, IMapper mapper)
        {
            _engineService = engineService;
           _mapper = mapper;
        }
        // GET: Engine
        public async Task<ActionResult> Index()
        {
            var listDTOEngine =  await _engineService.GetAllAsync();
            var listViewEngine = _mapper.Map<IEnumerable<EngineView>>(listDTOEngine);
            return View(listViewEngine);
        }

        // GET: Engine/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var findEngineDTO= await _engineService.FindByIdAsync(id);
            var engine = _mapper.Map<EngineView>(findEngineDTO);
            return View(engine);
        }

        // GET: Engine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Engine/Create
        [HttpPost]
        public async Task<ActionResult> Create(EngineView newEngine)
        {
            if (ModelState.IsValid)
            {
                var newBLEngine = _mapper.Map<EngineDTO>(newEngine);
                await _engineService.CreateAsync(newBLEngine);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Not Valid";
                return View(newEngine);
            }
        }

        // GET: Engine/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var engineDTO = await _engineService.FindByIdAsync(id);
            var engineView = _mapper.Map<EngineView>(engineDTO);
            return View(engineView);
        }

        // POST: Engine/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(EngineView editEngine)
        {
            if (ModelState.IsValid)
            {
                var newBLEngine = _mapper.Map<EngineDTO>(editEngine);
                await _engineService.UpdateAsync(newBLEngine);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Not Valid";
                return View(editEngine);
            }
        }

        // GET: Engine/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Engine/Delete/5
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
