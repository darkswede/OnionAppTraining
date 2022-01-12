using Microsoft.AspNetCore.Mvc;
using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.User;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsers();

            return Json(users);
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
            await DispatchAsync(command);

            return Created($"user/{command.Email}", null);
        }
    }
}
