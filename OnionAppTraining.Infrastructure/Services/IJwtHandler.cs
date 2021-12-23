using OnionAppTraining.Infrastructure.DTO;
using System;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDTO CreateToken(string userId, string role); 
    }
}
