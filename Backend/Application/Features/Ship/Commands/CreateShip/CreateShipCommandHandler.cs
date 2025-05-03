using Application.DTOs;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ship.Commands.CreateShip
{
    public class CreateShipCommandHandler : IRequestHandler<CreateShipCommand, Result<Guid>>
    {
        private readonly IShipRepository _shipRepository;
        private readonly IMapper mapper;
        public CreateShipCommandHandler(IShipRepository shipRepository, IMapper mapper)
        {
            _shipRepository = shipRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateShipCommand request, CancellationToken cancellationToken)
        {

            var ship = mapper.Map<Domain.Entities.Ship>(request);

            var result = await _shipRepository.AddAsync(ship);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }

            return Result<Guid>.Failure(result.ErrorMessage);
        }
    }
}
