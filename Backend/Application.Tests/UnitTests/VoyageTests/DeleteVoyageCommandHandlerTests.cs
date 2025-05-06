using Application.Features.Voyage.Commands.DeleteVoyage;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tests.UnitTests.VoyageTests
{
    public class DeleteVoyageCommandHandlerTests
    {
        private readonly IVoyageRepository repository;
        private readonly DeleteVoyageCommandHandler handler;

        public DeleteVoyageCommandHandlerTests()
        {
            repository = Substitute.For<IVoyageRepository>();
            handler = new DeleteVoyageCommandHandler(repository);
        }

        [Fact]
        public async Task Given_ValidDeleteVoyageCommand_When_HandleIsCalled_Then_VoyageShouldBeDeleted()
        {
            // Arrange
            var voyageId = Guid.NewGuid();
            var command = new DeleteVoyageCommand { Id = voyageId };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).DeleteAsync(voyageId);
            result.Should().Be(Unit.Value);
        }

        [Fact]
        public async Task Given_InvalidDeleteVoyageCommand_When_HandleIsCalled_Then_RepositoryIsCalledWithEmptyGuid()
        {
            // Arrange
            var invalidCommand = new DeleteVoyageCommand { Id = Guid.Empty };

            // Act
            var result = await handler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            await repository.Received(1).DeleteAsync(Guid.Empty);
            result.Should().Be(Unit.Value);
        }
    }
}
