using Application.DTOs;
using MediatR;

namespace Application.Features.Port.Queries.GetMostArrivals
{
    

    public class GetMostArrivalsQuery : IRequest<List<MostArrivalsDTO>>
    {
    }

}
