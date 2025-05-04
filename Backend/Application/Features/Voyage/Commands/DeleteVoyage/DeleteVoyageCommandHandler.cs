using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Voyage.Commands.DeleteVoyage
{
    public class DeleteVoyageCommandHandler : IRequestHandler<DeleteVoyageCommand, Unit>
    {
        private readonly IVoyageRepository _voyageRepository;
        public DeleteVoyageCommandHandler(IVoyageRepository voyageRepository)
        {
            _voyageRepository = voyageRepository;
        }
        public async Task<Unit> Handle(DeleteVoyageCommand request, CancellationToken cancellationToken)
        { 
          await _voyageRepository.DeleteAsync(request.Id);
            return Unit.Value;

        }
    }
    
}
