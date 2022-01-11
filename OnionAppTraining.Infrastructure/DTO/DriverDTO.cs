using System;

namespace OnionAppTraining.Infrastructure.DTO
{
    public class DriverDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public VehicleDTO Vehicle { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
