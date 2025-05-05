using Application.DTOs;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Queries.GetAverageDurationPerMonth
{
    public class GetAverageDurationPerMonthHandler
    : IRequestHandler<GetAverageDurationPerMonthQuery, List<AverageDurationPerMonthDTO>>
    {
        private readonly NapaDbContext _context;

        public GetAverageDurationPerMonthHandler(NapaDbContext context)
        {
            _context = context;
        }

        public async Task<List<AverageDurationPerMonthDTO>> Handle(GetAverageDurationPerMonthQuery request, CancellationToken cancellationToken)
        {
            var oneYearAgo = DateTime.UtcNow.AddYears(-1);

            var voyages = await _context.Voyages
                .Where(v => v.Start >= oneYearAgo && v.End > v.Start)
                .ToListAsync(cancellationToken);

            var result = voyages
                .GroupBy(v => new { v.Start.Year, v.Start.Month })
                .Select(g => new AverageDurationPerMonthDTO
                {
                    Month = $"{g.Key.Year:D4}-{g.Key.Month:D2}",
                    AverageDuration = Math.Round(g.Average(v => (v.End - v.Start).TotalDays), 2)
                })
                .OrderBy(x => x.Month)
                .ToList();

            return result;
        }
    }

}
