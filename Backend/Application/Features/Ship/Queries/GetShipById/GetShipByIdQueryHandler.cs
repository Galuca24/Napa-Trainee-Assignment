using Application.DTOs;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ship.Queries.GetShipById
{
    public class GetShipByIdQueryHandler : IRequestHandler<GetShipByIdQuery, ShipDTO>
    {
        private readonly IShipRepository _shipRepository;
        private readonly IMapper mapper;

        public GetShipByIdQueryHandler(IShipRepository shipRepository, IMapper mapper)
        {
            _shipRepository = shipRepository;
            this.mapper = mapper;
        }

        public async Task<ShipDTO> Handle(GetShipByIdQuery request, CancellationToken cancellationToken)
        {
            var ship = await _shipRepository.GetByIdAsync(request.Id);
            return mapper.Map<ShipDTO>(ship);
        }
    }
}
