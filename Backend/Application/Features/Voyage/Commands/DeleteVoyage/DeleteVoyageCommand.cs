using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Commands.DeleteVoyage
{
    public class DeleteVoyageCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
