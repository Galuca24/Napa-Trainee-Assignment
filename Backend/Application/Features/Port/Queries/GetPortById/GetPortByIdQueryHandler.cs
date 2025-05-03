using Application.DTOs;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Port.Queries.GetPortById
{
    public class GetPortByIdQueryHandler : IRequestHandler<GetPortByIdQuery,PortDTO>
    {
        private readonly IPortRepository _portRepository;
        private readonly IMapper _mapper;

        public GetPortByIdQueryHandler(IPortRepository portRepository, IMapper mapper)
        {
            _portRepository = portRepository;
            _mapper = mapper;
        }

        public async Task<PortDTO> Handle(GetPortByIdQuery request, CancellationToken cancellationToken)
        {
            var port = await _portRepository.GetByIdAsync(request.Id);
            return _mapper.Map<PortDTO>(port);
        }
    }
}
