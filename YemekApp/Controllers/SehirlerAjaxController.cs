using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    
    [Route("[controller]")] // ~/SehirlerAjax
    public class SehirlerAjaxController : Controller
    {
        private readonly ISehirService _sehirService;

        public SehirlerAjaxController(ISehirService sehirService)
        {
            _sehirService = sehirService;
        }

        
        [Route("SehirlerGet/{ulkeId?}")] // ~/SehirlerAjax/SehirlerGet/1
        public IActionResult SehirlerGet(int? ulkeId)
        {
            if (!ulkeId.HasValue)
                return NotFound();

            
            var result = _sehirService.SehirleriGetir(ulkeId.Value);

            return Json(result.Data);
        }

        [Route("SehirlerPost/{ulkeId?}")] // ~/SehirlerAjax/SehirlerPost
        [HttpPost]
        public IActionResult SehirlerPost(int? ulkeId)
        {
            if (!ulkeId.HasValue)
                return NotFound();
            var result = _sehirService.SehirleriGetir(ulkeId.Value);
            return Json(result.Data);
        }
    }
}