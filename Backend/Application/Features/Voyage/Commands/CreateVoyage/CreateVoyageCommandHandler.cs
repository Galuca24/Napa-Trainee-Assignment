using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Commands.CreateVoyage
{
    public class CreateVoyageCommandHandler : IRequestHandler<CreateVoyageCommand, Result<Guid>>
    {
        private readonly IVoyageRepository _voyageRepository;
        private readonly IMapper mapper;

        public CreateVoyageCommandHandler(IVoyageRepository voyageRepository, IMapper mapper)
        {
            _voyageRepository = voyageRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateVoyageCommand request, CancellationToken cancellationToken)
        {
            var voyage = mapper.Map<Domain.Entities.Voyage>(request);

            var result = await _voyageRepository.AddAsync(voyage);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }

            return Result<Guid>.Failure(result.ErrorMessage);
        }
    }
    
}
