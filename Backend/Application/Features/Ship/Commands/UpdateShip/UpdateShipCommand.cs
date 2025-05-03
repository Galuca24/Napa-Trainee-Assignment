using Application.Features.Ship.Commands.CreateShip;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ship.Commands.UpdateShip
{
    public class UpdateShipCommand : CreateShipCommand,IRequest
    {
        public Guid Id { get; set; }
    }
}
