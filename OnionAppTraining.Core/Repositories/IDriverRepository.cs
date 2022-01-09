using OnionAppTraining.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionAppTraining.Core.Repositories
{
    public interface IDriverRepository : IRepository
    {
        Task<Driver> GetByIdAsync(Guid userId);
        Task<IEnumerable<Driver>> GetAllAsync();
        Task AddAsync(Driver driver);
        Task UpdateAsync(Driver driver);
        Task DeleteAsync(Driver driver);
    }
}
