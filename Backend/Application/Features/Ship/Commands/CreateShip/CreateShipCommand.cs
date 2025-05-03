using Application.DTOs;
using Domain.Common;
using MediatR;

namespace Application.Features.Ship.Commands.CreateShip
{
    public class CreateShipCommand : IRequest<Result<Guid>>
    {
        public string? Name { get; set; }
        public double MaxSpeed { get; set; }
    }

}
