using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Commands.CreateVoyage
{
    public class CreateVoyageCommand : IRequest<Result<Guid>>
    {
        public DateTime VoyageDate { get; set; }
        public Guid DeparturePortId { get; set; }
        public Guid ArrivalPortId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid ShipId { get; set; }
    }
}
