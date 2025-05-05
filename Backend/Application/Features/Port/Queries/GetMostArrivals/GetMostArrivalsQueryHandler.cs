using Application.DTOs;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Port.Queries.GetMostArrivals
{
    public class GetMostArrivalsQueryHandler : IRequestHandler<GetMostArrivalsQuery, List<MostArrivalsDTO>>
    {
        private readonly NapaDbContext _context;

        public GetMostArrivalsQueryHandler(NapaDbContext context)
        {
            _context = context;
        }

        public async Task<List<MostArrivalsDTO>> Handle(GetMostArrivalsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Voyages
                .Include(v => v.ArrivalPort)
                .Where(v => v.ArrivalPort != null)
                .GroupBy(v => v.ArrivalPort!.Name)
                .Select(g => new MostArrivalsDTO
                {
                    PortName = g.Key,
                    ArrivalCount = g.Count()
                })
                .OrderByDescending(x => x.ArrivalCount)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
