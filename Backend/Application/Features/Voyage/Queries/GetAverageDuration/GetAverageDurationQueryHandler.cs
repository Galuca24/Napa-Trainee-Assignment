using Application.DTOs;
using Application.Features.Voyage.Queries.GetAverageDuration;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAverageDurationQueryHandler
    : IRequestHandler<GetAverageDurationQuery, AverageDurationDTO>
{
    private readonly NapaDbContext _context;

    public GetAverageDurationQueryHandler(NapaDbContext context)
    {
        _context = context;
    }

    public async Task<AverageDurationDTO> Handle(GetAverageDurationQuery request, CancellationToken cancellationToken)
    {
        var voyages = await _context.Voyages
            .Where(v => v.End > v.Start)
            .ToListAsync(cancellationToken);

        var averageDays = voyages
            .Select(v => (v.End - v.Start).TotalDays)
            .Average();

        return new AverageDurationDTO
        {
            AverageDurationDays = Math.Round(averageDays, 2)
        };
    }

}
