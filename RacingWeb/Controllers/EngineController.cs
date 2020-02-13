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
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> GetEngines()
        {
            var listDTOEngines = await _engineService.GetAllAsync();
            var listViewEngines = _mapper.Map<IEnumerable<EngineView>>(listDTOEngines);
            return Json(new { data = listViewEngines }, JsonRequestBehavior.AllowGet);
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
                return View(new EngineView());
            }
            var engineDTO = await _engineService.FindByIdAsync(id);
            if (engineDTO != null)
            {
                var engineView = _mapper.Map<EngineView>(engineDTO);
                return View(engineView);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(EngineView engine)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var updDTOEngine = _mapper.Map<EngineDTO>(engine);
                if (engine.Id > 0)
                {
                    //Edit 
                    await _engineService.UpdateAsync(updDTOEngine);
                }
                else
                {
                    //Save
                    await _engineService.CreateAsync(updDTOEngine);
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
            var engineToDeleteDTO = await _engineService.FindByIdAsync(id);
            if (engineToDeleteDTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var engineToDeleteView = _mapper.Map<EngineView>(engineToDeleteDTO);
            return View(engineToDeleteView);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            bool status = false;
            var deleteResult = await _engineService.RemoveAsync(id);
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
        //async doestn work
        private async Task<bool> IsModelNameOccupedAsync(string modelName)
        {
            var getItemByModel = await _engineService.FindByModelAsync(modelName);
            return getItemByModel != null ? true : false;
        }
        private bool IsModelNameOccuped(string modelName)
        {
            var getItemByModel = _engineService.FindByModel(modelName);
            return getItemByModel != null ? false : true;
        }     
    }

}
