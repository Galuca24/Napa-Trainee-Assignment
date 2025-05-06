using Application.DTOs;
using Application.Features.Voyage.Queries.GetAllVoyage;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tests.UnitTests.VoyageTests
{
    public class GetVoyagesQueryHandlerTests
    {
        private readonly IVoyageRepository repository;
        private readonly IMapper mapper;

        public GetVoyagesQueryHandlerTests()
        {
            repository = Substitute.For<IVoyageRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public void Given_GetVoyagesQueryHandler_When_HandleIsCalled_Then_AListOfVoyagesShouldBeReturned()
        {
            // Arrange
            List<Voyage> voyages = GenerateVoyages();
            repository.GetAllVoyagesAsync().Returns(voyages);
            var query = new GetAllVoyageQuery();
            GenerateVoyagesDto(voyages);

            // Act
            var handler = new GetAllVoyageQueryHandler(repository, mapper);
            var result = handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            Assert.Equal(2, result.Result.Count);
            Assert.Equal(voyages[0].Id, result.Result[0].Id);
        }

        private void GenerateVoyagesDto(List<Voyage> voyages)
        {
            mapper.Map<List<VoyageDTO>>(voyages).Returns(new List<VoyageDTO>
            {
                new VoyageDTO
                {
                    Id = voyages[0].Id,
                    VoyageDate = voyages[0].VoyageDate,
                    DeparturePortId = voyages[0].DeparturePortId,
                    ArrivalPortId = voyages[0].ArrivalPortId,
                    Start = voyages[0].Start,
                    End = voyages[0].End,
                    ShipId = voyages[0].ShipId
                },
                new VoyageDTO
                {
                    Id = voyages[1].Id,
                    VoyageDate = voyages[1].VoyageDate,
                    DeparturePortId = voyages[1].DeparturePortId,
                    ArrivalPortId = voyages[1].ArrivalPortId,
                    Start = voyages[1].Start,
                    End = voyages[1].End,
                    ShipId = voyages[1].ShipId
                }
            });
        }

        private List<Voyage> GenerateVoyages()
        {
            return new List<Voyage>
            {
                new Voyage
                {
                    Id = Guid.NewGuid(),
                    VoyageDate = DateTime.UtcNow,
                    DeparturePortId = Guid.NewGuid(),
                    ArrivalPortId = Guid.NewGuid(),
                    Start = DateTime.UtcNow.AddHours(1),
                    End = DateTime.UtcNow.AddHours(5),
                    ShipId = Guid.NewGuid()
                },
                new Voyage
                {
                    Id = Guid.NewGuid(),
                    VoyageDate = DateTime.UtcNow.AddDays(1),
                    DeparturePortId = Guid.NewGuid(),
                    ArrivalPortId = Guid.NewGuid(),
                    Start = DateTime.UtcNow.AddDays(1).AddHours(1),
                    End = DateTime.UtcNow.AddDays(1).AddHours(5),
                    ShipId = Guid.NewGuid()
                }
            };
        }
    }
}
