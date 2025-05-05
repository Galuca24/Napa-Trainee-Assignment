using Application.DTOs;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Queries.GetVoyageCountPerMonth
{
    public class GetVoyageCountPerMonthQueryHandler
     : IRequestHandler<GetVoyageCountPerMonthQuery, List<VoyageCountPerMonthDTO>>
    {
        private readonly NapaDbContext _context;

        public GetVoyageCountPerMonthQueryHandler(NapaDbContext context)
        {
            _context = context;
        }

        public async Task<List<VoyageCountPerMonthDTO>> Handle(GetVoyageCountPerMonthQuery request, CancellationToken cancellationToken)
        {
            var oneYearAgo = DateTime.UtcNow.AddYears(-1);

            return _context.Voyages
                .Where(v => v.Start >= oneYearAgo && v.Start <= DateTime.UtcNow)
                .AsEnumerable()
                .GroupBy(v => new { v.Start.Year, v.Start.Month })
                .Select(g => new VoyageCountPerMonthDTO
                {
                    Month = $"{g.Key.Year:D4}-{g.Key.Month:D2}",
                    Count = g.Count()
                })
                .OrderBy(x => x.Month)
                .ToList();
        }


    }

}
