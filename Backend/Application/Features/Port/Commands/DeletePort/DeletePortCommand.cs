using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Port.Commands.DeletePort
{
    public class DeletePortCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

       
    }
}
