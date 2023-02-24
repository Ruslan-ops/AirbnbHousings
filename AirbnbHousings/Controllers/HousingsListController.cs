using Microsoft.AspNetCore.Mvc;

namespace AirbnbHousings.Controllers
{
    [ApiController]
    [Route("housings/[action]")]
    public class HousingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(Name="list")]
        public List<HousingShort> GetList()
        {
            return null;
        }
    }
}
