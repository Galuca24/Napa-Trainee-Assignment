using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Port.Commands.CreatePort
{
    public class CreatePortCommandHandler : IRequestHandler<CreatePortCommand, Result<Guid>>
    {
        private readonly IPortRepository _portRepository;
        private readonly IMapper _mapper;

        public CreatePortCommandHandler(IPortRepository portRepository, IMapper mapper)
        {
            _portRepository = portRepository;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreatePortCommand request, CancellationToken cancellationToken)
        {
            var port = _mapper.Map<Domain.Entities.Port>(request);
            var result = await _portRepository.AddAsync(port);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }
            return Result<Guid>.Failure(result.ErrorMessage);

        }
    }
    
}
