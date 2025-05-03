using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Port.Commands.DeletePort
{
    public class DeletePortCommandHandler : IRequestHandler<DeletePortCommand, Unit>
    {

        private readonly IPortRepository _portRepository;

        public DeletePortCommandHandler(IPortRepository portRepository)
        {
            _portRepository = portRepository;
        }

        public async Task<Unit> Handle(DeletePortCommand request, CancellationToken cancellationToken)
        {
            await _portRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
    
}
