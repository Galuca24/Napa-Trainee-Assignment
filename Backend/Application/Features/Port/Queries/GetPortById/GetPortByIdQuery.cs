using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Port.Queries.GetPortById
{
    public class GetPortByIdQuery : IRequest<PortDTO>
    {
        public Guid Id { get; set; }
    }
}
