using Application.DTOs;
using Application.Features.Ship.Queries.GetShipById;
using AutoMapper;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.UnitTests.ShipTests
{
    public class GetShipByIdQueryHandlerTest
    {
        private readonly IShipRepository repository;
        private readonly IMapper mapper;
        private readonly GetShipByIdQueryHandler handler;

        public GetShipByIdQueryHandlerTest()
        {
            repository = Substitute.For<IShipRepository>();
            mapper = Substitute.For<IMapper>();
            handler = new GetShipByIdQueryHandler(repository, mapper);
        }

        [Fact]
        public async Task Given_ValidGetShipByIdQuery_When_HandleIsCalled_Then_ShipShouldBeReturned()
        {
            // Arrange
            var shipId = Guid.NewGuid();
            var query = new GetShipByIdQuery { Id = shipId };
            var shipEntity = new Domain.Entities.Ship
            {
                Id = shipId,
                Name = "Titanic",
                MaxSpeed = 50
            };

            repository.GetByIdAsync(shipId).Returns(shipEntity);
            mapper.Map<ShipDTO>(shipEntity).Returns(new ShipDTO
            {
                Id = shipEntity.Id,
                Name = shipEntity.Name,
                MaxSpeed = shipEntity.MaxSpeed
            });

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(shipId);
        }
    }
}
