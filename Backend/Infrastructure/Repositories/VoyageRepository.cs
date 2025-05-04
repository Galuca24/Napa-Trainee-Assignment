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
    public class VoyageRepository : IVoyageRepository
    {
        private readonly NapaDbContext context;
        public VoyageRepository(NapaDbContext context)
        {
            this.context = context;
        }
        public async Task<Result<Guid>> AddAsync(Voyage voyage)
        {
            try
            {
                await context.Voyages.AddAsync(voyage);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(voyage.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.InnerException!.ToString());
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var voyage = await context.Voyages.FindAsync(id);
            if (voyage != null)
            {
                context.Voyages.Remove(voyage);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Voyage>> GetAllVoyagesAsync()
        {
            return await context.Voyages.ToListAsync();
        }

        public async Task<Voyage> GetByIdAsync(Guid id)
        {
            return await context.Voyages.FindAsync(id);
        }

        public Task UpdateAsync(Voyage voyage)
        {
            context.Voyages.Update(voyage);
            return context.SaveChangesAsync();
        }

        //public async Task<List<string>> GetVisitedCountriesLastYearAsync()
        //{
        //    var oneYearAgo = DateTime.UtcNow.AddYears(-1);

        //    return await context.Voyages
        //        .Where(v => v.End >= oneYearAgo)
        //        .Include(v => v.ArrivalPort)
        //        .Select(v => v.ArrivalPort!.Country)
        //        .Distinct()
        //        .ToListAsync();
        //}

    }
}
