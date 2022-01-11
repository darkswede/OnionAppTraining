using System.Collections.Generic;

namespace OnionAppTraining.Infrastructure.DTO
{
    public class DriverDetailsDTO : DriverDTO
    {
        public IEnumerable<RouteDTO> Routes { get; set; }
    }
}
