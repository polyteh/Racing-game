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
    public class SuspentionController : Controller
    {
        private readonly ISuspentionService _suspentionService;
        private readonly IMapper _mapper;
        [Inject]
        public SuspentionController(ISuspentionService suspentionService, IMapper mapper)
        {
            _suspentionService = suspentionService;
            _mapper = mapper;
        }
        // GET: Suspention
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> GetSuspentions()
        {
            var listDTOSuspection = await _suspentionService.GetAllAsync();
            var listViewSuspection = _mapper.Map<IEnumerable<SuspentionView>>(listDTOSuspection);
            return Json(new { data = listViewSuspection }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Save(int id)
        {
            if (id==0)
            {
                return View(new SuspentionView());
            }
            var suspentionDTO = await _suspentionService.FindByIdAsync(id);
            if (suspentionDTO != null)
            {
                var suspectionView = _mapper.Map<SuspentionView>(suspentionDTO);
                return View(suspectionView);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public async Task<ActionResult> Save(SuspentionView susp)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var updBLSuspention = _mapper.Map<SuspentionDTO>(susp);
                if (susp.Id > 0)
                {
                    //Edit 
                    await _suspentionService.UpdateAsync(updBLSuspention);
                }
                else
                {
                    //Save
                    await _suspentionService.CreateAsync(updBLSuspention);
                }
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }


    }
}
