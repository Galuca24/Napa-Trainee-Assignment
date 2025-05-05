using Application.Features.Ship.Commands.CreateShip;
using AutoMapper;
using Domain.Common;
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
    public class CreateCommandHandlerTests
    {

        private readonly IShipRepository repository;
        private readonly CreateShipCommandHandler handler;
        private readonly IMapper mapper;


        public CreateCommandHandlerTests()
        {
            repository = Substitute.For<IShipRepository>();
            mapper = Substitute.For<IMapper>();
            handler = new CreateShipCommandHandler(repository, mapper);
        }


        [Fact]
        public async Task Given_ValidCreateShipCommand_When_HandleIsCalled_Then_ShipShouldBeCreated()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                Name = "Titanic",
                MaxSpeed = 30
            };

            var shipEntity = new Domain.Entities.Ship
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                MaxSpeed = command.MaxSpeed
            };

            mapper.Map<Domain.Entities.Ship>(command).Returns(shipEntity);
            repository.AddAsync(shipEntity).Returns(Result<Guid>.Success(shipEntity.Id));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(shipEntity);
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task Given_InvalidCreateShipCommand_When_HandleIsCalled_Then_FailureResultShouldBeReturned()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                Name = "",
                MaxSpeed = -10
            };

            var shipEntity = new Domain.Entities.Ship
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                MaxSpeed = command.MaxSpeed
            };
            mapper.Map<Domain.Entities.Ship>(command).Returns(shipEntity);
            repository.AddAsync(shipEntity).Returns(Result<Guid>.Failure("Invalid ship data"));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(shipEntity);
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Invalid ship data");
        }

    }
}
