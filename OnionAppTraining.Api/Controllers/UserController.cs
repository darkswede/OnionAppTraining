using Microsoft.AspNetCore.Mvc;
using OnionAppTraining.Infrastructure.Commands.User;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Api.Controllers
{
    [Route("{controller}")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await _userService.RegisterAsync(command.Email, command.Password, command.Username);

            return Created($"user/{command.Email}", new object());
        }
    }
}
