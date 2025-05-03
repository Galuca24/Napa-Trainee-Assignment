using Application.DTOs;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Queries.GetByIdVoyage
{
    public class GetByIdVoyageQueryHandler : IRequestHandler<GetByIdVoyageQuery, VoyageDTO>
    {
        private readonly IVoyageRepository _voyageRepository;
        private readonly IMapper mapper;

        public GetByIdVoyageQueryHandler(IVoyageRepository voyageRepository, IMapper mapper)
        {
            _voyageRepository = voyageRepository;
            this.mapper = mapper;
        }

        public async Task<VoyageDTO> Handle(GetByIdVoyageQuery request, CancellationToken cancellationToken)
        {
            var voyage = await _voyageRepository.GetByIdAsync(request.Id);
            return mapper.Map<VoyageDTO>(voyage);
        }
    }
    
}
