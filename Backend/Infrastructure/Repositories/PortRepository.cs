using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PortRepository : IPortRepository
    {
        private readonly NapaDbContext context;

        public PortRepository(NapaDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<Guid>> AddAsync(Port port)
        {
            try
            {
                await context.Ports.AddAsync(port);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(port.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.InnerException!.ToString());
            }

        }

        public async Task DeleteAsync(Guid id)
        {
            var port = await context.Ports.FindAsync(id);
            if (port != null)
            {
                context.Ports.Remove(port);
                await context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Port>> GetAllPortsAsync()
        {
            return await context.Ports.ToListAsync();
        }

        public async Task<Port> GetByIdAsync(Guid id)
        {
            return await context.Ports.FindAsync(id);
        }

        public  Task UpdateAsync(Port port)
        {
            context.Ports.Update(port);
            return context.SaveChangesAsync();
        }



    }
}
