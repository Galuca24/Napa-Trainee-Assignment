using Application.Features.Port.Commands.DeletePort;
using Application.Tests.UnitTests.ShipTests;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.UnitTests.PortTests
{
    public class DeletePortCommandHandlerTests
    {
        private readonly IPortRepository repository;
        private readonly DeletePortCommandHandler handler;

        public DeletePortCommandHandlerTests()
        {
            repository = Substitute.For<IPortRepository>();
            handler = new DeletePortCommandHandler(repository);
        }

        [Fact]
        public async Task Given_ValidDeletePortCommand_When_HandleIsCalled_Then_PortShouldBeDeleted()
        {
            // Arrange
            var portId = Guid.NewGuid();
            var command = new DeletePortCommand { Id = portId };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).DeleteAsync(portId);
            result.Should().Be(Unit.Value);
        }


        [Fact]
        public async Task Given_InvalidDeletePortCommand_When_HandleIsCalled_Then_RepositoryIsCalledWithEmptyGuid()
        {
            // Arrange
            var invalidCommand = new DeletePortCommand { Id = Guid.Empty };

            // Act
            var result = await handler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            await repository.Received(1).DeleteAsync(Guid.Empty);
            result.Should().Be(Unit.Value);
        }

    }
}
