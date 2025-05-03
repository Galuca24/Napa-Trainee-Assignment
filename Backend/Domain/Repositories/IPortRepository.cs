using Domain.Common;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPortRepository
    {
        Task<IEnumerable<Port>> GetAllPortsAsync();
        Task<Port> GetByIdAsync(Guid id);
        Task<Result<Guid>> AddAsync(Port port);
        Task UpdateAsync(Port port);
        Task DeleteAsync(Guid id);
    }
}
