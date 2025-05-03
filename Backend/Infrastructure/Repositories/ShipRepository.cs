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
    public class ShipRepository : IShipRepository
    {
        private readonly NapaDbContext context;

        public ShipRepository(NapaDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<Guid>> AddAsync(Domain.Entities.Ship ship)
        {
            try
            {
                await context.Ships.AddAsync(ship);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(ship.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.InnerException!.ToString());


            }

        }

        public async Task DeleteAsync(Guid id)
        {
            var ship = await context.Ships.FindAsync(id);
            if (ship != null)
            {
                context.Ships.Remove(ship);
                await context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Ship>> GetAllShipsAsync()
        {
            return await context.Ships.ToListAsync();
        }

        public async Task<Ship> GetByIdAsync(Guid id)
        {
            return await context.Ships.FindAsync(id);
        }

        public  Task UpdateAsync(Ship ship)
        {
            context.Ships.Update(ship);
            return context.SaveChangesAsync();
        }

    }
}
