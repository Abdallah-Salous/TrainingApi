using Microsoft.AspNetCore.Mvc;

namespace TrainingAPi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult GetUserName()
        {
            return new OkObjectResult("Abdallah");
        }


        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            return new OkObjectResult("Abdallah");
        }


    }
}
