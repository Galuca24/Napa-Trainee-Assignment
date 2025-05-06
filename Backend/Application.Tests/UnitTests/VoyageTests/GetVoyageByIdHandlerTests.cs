using Application.DTOs;
using Application.Features.Voyage.Queries.GetByIdVoyage;
using AutoMapper;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace Application.Tests.UnitTests.VoyageTests
{
    public class GetVoyageByIdHandlerTests
    {
        private readonly IVoyageRepository repository;
        private readonly IMapper mapper;
        private readonly GetByIdVoyageQueryHandler handler;

        public GetVoyageByIdHandlerTests()
        {
            repository = Substitute.For<IVoyageRepository>();
            mapper = Substitute.For<IMapper>();
            handler = new GetByIdVoyageQueryHandler(repository, mapper);
        }

        [Fact]
        public async Task Given_ValidGetVoyageByIdQuery_When_HandleIsCalled_Then_VoyageShouldBeReturned()
        {
            // Arrange
            var voyageId = Guid.NewGuid();
            var query = new GetByIdVoyageQuery { Id = voyageId };
            var voyageEntity = new Domain.Entities.Voyage
            {
                Id = voyageId,
                VoyageDate = DateTime.UtcNow,
                DeparturePortId = Guid.NewGuid(),
                ArrivalPortId = Guid.NewGuid(),
                Start = DateTime.UtcNow.AddHours(1),
                End = DateTime.UtcNow.AddHours(5),
                ShipId = Guid.NewGuid()
            };

            repository.GetByIdAsync(voyageId).Returns(voyageEntity);
            mapper.Map<VoyageDTO>(voyageEntity).Returns(new VoyageDTO
            {
                Id = voyageEntity.Id,
                VoyageDate = voyageEntity.VoyageDate,
                DeparturePortId = voyageEntity.DeparturePortId,
                ArrivalPortId = voyageEntity.ArrivalPortId,
                Start = voyageEntity.Start,
                End = voyageEntity.End,
                ShipId = voyageEntity.ShipId
            });

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(voyageId);
        }
    }
}
