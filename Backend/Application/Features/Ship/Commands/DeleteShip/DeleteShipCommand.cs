using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ship.Commands.DeleteShip
{
    public class DeleteShipCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
