using Microsoft.AspNetCore.Mvc;
using OnionAppTraining.Infrastructure.Commands;

namespace OnionAppTraining.Api.Controllers
{
    [Route("{controller}")]
    public abstract class ApiControllerBase : Controller
    {
        protected readonly ICommandDispatcher CommandDispatcher;

        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }
    }
}
