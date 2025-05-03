using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ship.Commands.DeleteShip
{
    public class DeleteShipCommandHandler : IRequestHandler<DeleteShipCommand, Unit>
    {
        private readonly IShipRepository _shipRepository;
        public DeleteShipCommandHandler(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }
        public async Task<Unit> Handle(DeleteShipCommand request, CancellationToken cancellationToken)
        {
            
            await _shipRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
    
}
