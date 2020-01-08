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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brake/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Brake/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
