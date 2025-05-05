using Application.Features.Ship.Commands.DeleteShip;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.UnitTests.ShipTests
{
    public class DeleteShipCommandHandlerTests
    {
        private readonly IShipRepository repository;
        private readonly DeleteShipCommandHandler handler;

        public DeleteShipCommandHandlerTests()
        {
            repository = Substitute.For<IShipRepository>();
            handler = new DeleteShipCommandHandler(repository);
        }

        [Fact]
        public async Task Given_ValidDeleteShipCommand_When_HandleIsCalled_Then_ShipShouldBeDeleted()
        {
            //Arrange
            var shipId = Guid.NewGuid();
            var command = new DeleteShipCommand { Id = shipId };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            await repository.Received(1).DeleteAsync(shipId);
            result.Should().Be(Unit.Value);
        }

        [Fact]
        public async Task Given_InvalidDeleteShipCommand_When_HandleIsCalled_Then_RepositoryIsCalledWithEmptyGuid()
        {
            // Arrange
            var invalidCommand = new DeleteShipCommand { Id = Guid.Empty };

            // Act
            var result = await handler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            await repository.Received(1).DeleteAsync(Guid.Empty);
            result.Should().Be(Unit.Value);
        }


    }
}
