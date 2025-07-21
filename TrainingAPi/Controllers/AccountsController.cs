using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TrainingAPi.Extesnions2;
using TrainingAPi.Shared;
using TrainingAPi.ViewModel2;
using TrainingApiDAL.Models;
using TrainingApiDAL.Repositories;

namespace TrainingAPi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAppUserRepositry _userRepository;
        private readonly IValidator<AppUserVM> _validator;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountsController> _logger;
        public AccountsController(IAppUserRepositry userRepository,
            IValidator<AppUserVM> validator,
            IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            _userRepository = userRepository;
            _validator = validator;
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<AccountsController>();
        }

        [ProducesResponseType(typeof(AppUserVM), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [HttpPost("NewUser")]
        [Authorize(Roles ="Admin, User")]
        public async Task<IActionResult> RegisterNewUser(AppUserVM user)
        {
            var validateRes = _validator.Validate(user);
            if (!validateRes.IsValid)
            {
                return BadRequest(validateRes.Errors);
            }

            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var appUser = user.Adapt<AppUser>();
            var addedUser = await _userRepository.AddUserAsync(appUser);
            return Ok(addedUser.Adapt<AppUserVM>());
        }

        [AllowAnonymous]
        [HttpPost("Token")]
        [ProducesResponseType(typeof(TokenVM), 200)]
        public async Task<IActionResult> Token([FromForm] LoginViewModel model)
        {
            var user = await _userRepository.GetUserByName(model.UserName);
            //_logger.log
            if (user == null)
            {

                return NotFound("");
                //throw new TrainingBadRequestException("Cannot find user");
            }

            var token = GenerateAuthToken(user);
            var response = new TokenVM
            {
                Access_token = token,
                Token_type = "bearer",
                Expires_in = (int)_configuration.GetSection("JWTConfig").GetValue<double>("ValidMins") * 60
            };
            return Ok(response);
        }

        private string GenerateAuthToken(AppUser appuser)
        {
            var user = new ClaimsPrincipal(
                       new ClaimsIdentity(
                           new Claim[] {
                                 new Claim(ClaimTypes.NameIdentifier, appuser.Id.ToString()),
                                 new Claim(ClaimTypes.Role, "User"),
                                 new Claim("IsAdmin", "true")
                           })
            );

            var securityToken = user.GenerateToken(_configuration);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }


    }
}
