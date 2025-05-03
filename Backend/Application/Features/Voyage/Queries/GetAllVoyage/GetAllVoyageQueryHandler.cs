using Application.DTOs;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Queries.GetAllVoyage
{
    public class GetAllVoyageQueryHandler : IRequestHandler<GetAllVoyageQuery, List<VoyageDTO>>
    {

        private readonly IVoyageRepository _voyageRepository;
        private readonly IMapper _mapper;

        public GetAllVoyageQueryHandler(IVoyageRepository voyageRepository, IMapper mapper)
        {
            _voyageRepository = voyageRepository;
            _mapper = mapper;
        }
        public Task<List<VoyageDTO>> Handle(GetAllVoyageQuery request, CancellationToken cancellationToken)
        {
            var voyages = _voyageRepository.GetAllVoyagesAsync();
            return Task.FromResult(_mapper.Map<List<VoyageDTO>>(voyages.Result));

        }
    }
    
}
