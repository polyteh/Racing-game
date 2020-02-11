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
    }
}
