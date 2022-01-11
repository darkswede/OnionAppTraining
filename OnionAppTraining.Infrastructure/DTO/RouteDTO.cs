using OnionAppTraining.Core.Domain;

namespace OnionAppTraining.Infrastructure.DTO
{
    public class RouteDTO
    {
        public string Name { get; set; }
        public Node Start { get; set; }
        public Node End { get; set; }
        public double Distance { get; set; }
    }
}
