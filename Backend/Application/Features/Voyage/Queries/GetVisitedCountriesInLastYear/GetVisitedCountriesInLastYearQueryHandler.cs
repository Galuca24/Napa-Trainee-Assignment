using Application.DTOs;
using Domain.Repositories;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Queries.GetVisitedCountriesInLastYear
{
    public class GetVisitedCountriesDetailedQueryHandler : IRequestHandler<GetVisitedCountriesQuery, List<VisitedCountryDTO>>
    {
        private readonly NapaDbContext _context;

        public GetVisitedCountriesDetailedQueryHandler(NapaDbContext context)
        {
            _context = context;
        }

        public async Task<List<VisitedCountryDTO>> Handle(GetVisitedCountriesQuery request, CancellationToken cancellationToken)
        {
            var oneYearAgo = DateTime.UtcNow.AddYears(-1);

            return await _context.Voyages
                .Where(v => v.End >= oneYearAgo && v.End <= DateTime.UtcNow)
                .Include(v => v.ArrivalPort)
                .Select(v => new VisitedCountryDTO
                {
                    Country = v.ArrivalPort!.Country,
                    ShipId = v.ShipId,
                    VoyageId = v.Id,
                    Start = v.Start,
                    End = v.End
                })
                .ToListAsync(cancellationToken);
        }
    }
}
