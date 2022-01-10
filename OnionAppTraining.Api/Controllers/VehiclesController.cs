using Microsoft.AspNetCore.Mvc;
using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Api.Controllers
{
    public class VehiclesController : ApiControllerBase
    {
        private readonly IVehicleProvider _vehicleProvider;

        public VehiclesController(ICommandDispatcher commandDispatcher, IVehicleProvider vehicleProvider) : base(commandDispatcher)
        {
            _vehicleProvider = vehicleProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _vehicleProvider.GetAllAsync();

            return Json(vehicles);
        }
    }
}
