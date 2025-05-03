using Domain.Common;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IShipRepository
    {
        Task<IEnumerable<Ship>> GetAllShipsAsync();
        Task<Ship>GetByIdAsync(Guid id);
        Task<Result<Guid>>AddAsync(Ship ship);
        Task UpdateAsync(Ship ship);
        Task DeleteAsync(Guid id);
    }
}
