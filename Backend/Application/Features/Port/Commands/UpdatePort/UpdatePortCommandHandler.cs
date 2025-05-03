using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Port.Commands.UpdatePort
{
    public class UpdatePortCommandHandler : IRequestHandler<UpdatePortCommand>
    {

        private readonly IPortRepository _portRepository;
        private readonly IMapper mapper;

        public UpdatePortCommandHandler(IPortRepository portRepository, IMapper mapper)
        {
            _portRepository = portRepository;
            this.mapper = mapper;
        }



        public Task Handle(UpdatePortCommand request, CancellationToken cancellationToken)
        {
           var port = mapper.Map<Domain.Entities.Port>(request);
            return _portRepository.UpdateAsync(port);
        }
    }
    
}
