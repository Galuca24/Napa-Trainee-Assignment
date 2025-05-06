using Application.Features.Voyage.Commands.CreateVoyage;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tests.UnitTests.VoyageTests
{
    public class CreateVoyageCommandHandlerTests
    {
        private readonly IVoyageRepository repository;
        private readonly CreateVoyageCommandHandler handler;
        private readonly IMapper mapper;

        public CreateVoyageCommandHandlerTests()
        {
            repository = Substitute.For<IVoyageRepository>();
            mapper = Substitute.For<IMapper>();
            handler = new CreateVoyageCommandHandler(repository, mapper);
        }

        [Fact]
        public async Task Given_ValidCreateVoyageCommand_When_HandleIsCalled_Then_VoyageShouldBeCreated()
        {
            // Arrange
            var command = new CreateVoyageCommand
            {
                VoyageDate = DateTime.UtcNow,
                DeparturePortId = Guid.NewGuid(),
                ArrivalPortId = Guid.NewGuid(),
                Start = DateTime.UtcNow.AddHours(1),
                End = DateTime.UtcNow.AddHours(5),
                ShipId = Guid.NewGuid()
            };

            var voyageEntity = new Voyage
            {
                Id = Guid.NewGuid(),
                VoyageDate = command.VoyageDate,
                DeparturePortId = command.DeparturePortId,
                ArrivalPortId = command.ArrivalPortId,
                Start = command.Start,
                End = command.End,
                ShipId = command.ShipId
            };

            mapper.Map<Voyage>(command).Returns(voyageEntity);
            repository.AddAsync(voyageEntity).Returns(Result<Guid>.Success(voyageEntity.Id));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(voyageEntity);
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task Given_InvalidCreateVoyageCommand_When_HandleIsCalled_Then_FailureResultShouldBeReturned()
        {
            // Arrange
            var command = new CreateVoyageCommand
            {
                VoyageDate = DateTime.MinValue, 
                DeparturePortId = Guid.Empty,  
                ArrivalPortId = Guid.Empty,    
                Start = DateTime.MinValue,
                End = DateTime.MinValue,
                ShipId = Guid.Empty           
            };

            var voyageEntity = new Voyage
            {
                Id = Guid.NewGuid(),
                VoyageDate = command.VoyageDate,
                DeparturePortId = command.DeparturePortId,
                ArrivalPortId = command.ArrivalPortId,
                Start = command.Start,
                End = command.End,
                ShipId = command.ShipId
            };

            mapper.Map<Voyage>(command).Returns(voyageEntity);
            repository.AddAsync(voyageEntity).Returns(Result<Guid>.Failure("Invalid voyage data"));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(voyageEntity);
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Invalid voyage data");
        }
    }
}
