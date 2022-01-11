using AutoMapper;
using OnionAppTraining.Core.Domain;
using OnionAppTraining.Infrastructure.DTO;

namespace OnionAppTraining.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize() =>
            new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<User, UserDTO>();
                configuration.CreateMap<Driver, DriverDTO>();
                configuration.CreateMap<Driver, DriverDetailsDTO>();
                configuration.CreateMap<Vehicle, VehicleDTO>();
                configuration.CreateMap<Route, RouteDTO>();
            })
            .CreateMapper();
    }
}
