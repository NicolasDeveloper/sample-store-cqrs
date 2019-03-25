using Microsoft.AspNetCore.Mvc;

namespace SampleStoreCQRS.Services.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return Ok(new { version = "0.0.0.1" });
        }
    }
}
