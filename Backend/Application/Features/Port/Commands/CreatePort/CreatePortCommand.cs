using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Port.Commands.CreatePort
{
    public class CreatePortCommand : IRequest<Result<Guid>>
    {
        public string? Name { get; set; } 
        public string? Country { get; set; } 
    }
    
}
