using AutoMapper;
using OnionAppTraining.Core.Domain;
using OnionAppTraining.Infrastructure.DTO;

public class BaseMapperProfile : Profile
{
    public BaseMapperProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<Driver, DriverDTO>();
        CreateMap<Driver, DriverDetailsDTO>();
        CreateMap<Vehicle, VehicleDTO>();
        CreateMap<Route, RouteDTO>();
    }
}