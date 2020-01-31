using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Ninject;
using RacingDTO.Interfaces;
using RacingDTO.Models;
using RacingWeb.Models;
using RacingWeb.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var listDTOEngine = await _engineService.GetAllAsync();
            var listViewEngine = _mapper.Map<IEnumerable<EngineView>>(listDTOEngine);
            return View(listViewEngine);
        }

        // GET: Engine/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var findEngineDTO = await _engineService.FindByIdAsync(id);
            if (findEngineDTO != null)
            {
                var engine = _mapper.Map<EngineView>(findEngineDTO);
                return View(engine);
            }
            return HttpNotFound();
        }

        // GET: Engine/Create
        [Authorize]
        public ActionResult Create()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            return View();
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
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
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var engineDTO = await _engineService.FindByIdAsync(id);
            if (engineDTO != null)
            {
                var engineView = _mapper.Map<EngineView>(engineDTO);
                return View(engineView);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
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
        public async Task<ActionResult> Delete(int id)
        {
            var engineToDeleteDTO = await _engineService.FindByIdAsync(id);
            if (engineToDeleteDTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var engineToDeleteView = _mapper.Map<EngineView>(engineToDeleteDTO);
            return View(engineToDeleteView);
        }

        // POST: Engine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            var deleteResult = await _engineService.RemoveAsync(id);
            if (deleteResult)
            {
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
    }

}
