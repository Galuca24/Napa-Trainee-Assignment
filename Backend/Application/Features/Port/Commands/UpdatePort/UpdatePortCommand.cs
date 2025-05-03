using Application.Features.Port.Commands.CreatePort;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Port.Commands.UpdatePort
{
    public class UpdatePortCommand : CreatePortCommand, IRequest
    {
        public Guid Id { get; set; }
    }
    
}
