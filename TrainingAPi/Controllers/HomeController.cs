using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TrainingApiDAL.Models;
using TrainingApiDAL.Repositories;

namespace TrainingAPi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IAppUserRepositry _respository;
        private readonly IPostsRepository _postRespository;

        public HomeController(IAppUserRepositry respository, IPostsRepository postRespository)
        {
           _respository = respository;
            _postRespository = postRespository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserName()
        {
            var result = await _respository.GetAllUsersAsync();

            return Ok(result);
        }


        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(long id)
        {
            var result = await _respository.GetUserWithPosts(id);

            return Ok(result);
        }


    }
}
