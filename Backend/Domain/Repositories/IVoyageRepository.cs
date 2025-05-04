using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IVoyageRepository
    {
        Task<IEnumerable<Voyage>> GetAllVoyagesAsync();
        Task<Voyage> GetByIdAsync(Guid id);
        Task<Result<Guid>> AddAsync(Voyage voyage);
        Task UpdateAsync(Voyage voyage);
        Task DeleteAsync(Guid id);

    }
}
