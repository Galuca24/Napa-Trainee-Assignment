using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ship.Queries.GetMostUsedShips
{
    public class GetMostUsedShipsQuery : IRequest<List<ShipUsageDTO>>
    {
    }

}
