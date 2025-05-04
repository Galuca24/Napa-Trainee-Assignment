using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Commands.UpdateVoyage
{
    public class UpdateVoyageCommandHandler : IRequestHandler<UpdateVoyageCommand>
    {
        private readonly IVoyageRepository _voyageRepository;
        private readonly IMapper _mapper;

        public UpdateVoyageCommandHandler(IVoyageRepository voyageRepository, IMapper mapper)
        {
            _voyageRepository = voyageRepository;
            _mapper = mapper;
        }

        public Task Handle(UpdateVoyageCommand request, CancellationToken cancellationToken)
        {
            var voyage = _mapper.Map<Domain.Entities.Voyage>(request);

            return _voyageRepository.UpdateAsync(voyage);
        }
    }
    
}
