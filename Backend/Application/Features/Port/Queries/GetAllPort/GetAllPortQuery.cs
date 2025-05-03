using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Port.Queries.GetAllPort
{
    public class GetAllPortQuery : IRequest<List<PortDTO>>
    {
    }
}
