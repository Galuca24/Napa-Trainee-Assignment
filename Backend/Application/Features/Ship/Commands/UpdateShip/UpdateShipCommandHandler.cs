using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ship.Commands.UpdateShip
{
    public class UpdateShipCommandHandler : IRequestHandler<UpdateShipCommand>
    {
        private readonly IShipRepository _shipRepository;
        private readonly IMapper _mapper;

        public UpdateShipCommandHandler(IShipRepository shipRepository, IMapper mapper)
        {
            _shipRepository = shipRepository;
            _mapper = mapper;
        }


        public Task Handle(UpdateShipCommand request, CancellationToken cancellationToken)
        {
            var ship = _mapper.Map<Domain.Entities.Ship>(request);
           
            return _shipRepository.UpdateAsync(ship);
        }
    }
    
}
