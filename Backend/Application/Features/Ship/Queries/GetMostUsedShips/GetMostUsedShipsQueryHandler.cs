using Application.DTOs;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ship.Queries.GetMostUsedShips
{
    public class GetMostUsedShipsQueryHandler : IRequestHandler<GetMostUsedShipsQuery, List<ShipUsageDTO>>
    {
        private readonly NapaDbContext _context;

        public GetMostUsedShipsQueryHandler(NapaDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShipUsageDTO>> Handle(GetMostUsedShipsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Voyages
                .Include(v => v.Ship)
                .GroupBy(v => v.Ship!.Name)
                .Select(g => new ShipUsageDTO
                {
                    ShipName = g.Key,
                    VoyageCount = g.Count()
                })
                .OrderByDescending(x => x.VoyageCount)
                .ToListAsync(cancellationToken);
        }
    }

}
