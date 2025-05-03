using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ship.Queries.GetShipById
{
    public class GetShipByIdQuery : IRequest<ShipDTO>
    {
        public Guid Id { get; set; }



    }
}
