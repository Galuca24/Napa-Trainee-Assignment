using Application.DTOs;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ship.Queries.GetAllShip
{
    public class GetAllShipQueryHandler : IRequestHandler<GetAllShipQuery, List<ShipDTO>>
    {
        private readonly IShipRepository _shipRepository;
        private readonly IMapper mapper;

        public GetAllShipQueryHandler(IShipRepository shipRepository, IMapper mapper)
        {
            _shipRepository = shipRepository;
            this.mapper = mapper;
        }

        public async Task<List<ShipDTO>> Handle(GetAllShipQuery request, CancellationToken cancellationToken)
        {
            var ships = await _shipRepository.GetAllShipsAsync();
            return mapper.Map<List<ShipDTO>>(ships);
        }
    }
   
}
