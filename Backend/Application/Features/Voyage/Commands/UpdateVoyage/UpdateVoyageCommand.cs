using Application.Features.Voyage.Commands.CreateVoyage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Commands.UpdateVoyage
{
    public class UpdateVoyageCommand : CreateVoyageCommand, IRequest
    {
        public Guid Id { get; set; }
    }
}
