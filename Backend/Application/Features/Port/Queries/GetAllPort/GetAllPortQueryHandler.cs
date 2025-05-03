using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Port.Queries.GetAllPort
{
    public class GetAllPortQueryHandler : IRequestHandler<GetAllPortQuery, List<PortDTO>>
    {
        private readonly IPortRepository _portRepository;
        private readonly IMapper mapper;

        public GetAllPortQueryHandler(IPortRepository portRepository, IMapper mapper)
        {
            _portRepository = portRepository;
            this.mapper = mapper;
        }

        public async Task<List<PortDTO>> Handle(GetAllPortQuery request, CancellationToken cancellationToken)
        {
            var ports = await _portRepository.GetAllPortsAsync();
            return mapper.Map<List<PortDTO>>(ports);
        }

    }

}
