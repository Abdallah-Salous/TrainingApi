using Microsoft.AspNetCore.Mvc;
using TrainingApiDAL.Repositories;

namespace TrainingAPi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAppUserRepositry _userRepository;
        public AccountsController(IAppUserRepositry userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
