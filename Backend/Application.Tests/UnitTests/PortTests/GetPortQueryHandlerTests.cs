using Application.DTOs;
using Application.Features.Port.Queries.GetAllPort;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tests.UnitTests.PortTests
{
    public class GetPortQueryHandlerTests
    {
        private readonly IPortRepository repository;
        private readonly IMapper mapper;

        public GetPortQueryHandlerTests()
        {
            repository = Substitute.For<IPortRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public void Given_GetPortQueryHandler_When_HandleIsCalled_Then_AListOfPortsShouldBeReturned()
        {
            // Arrange
            List<Port> ports = GeneratePorts();
            repository.GetAllPortsAsync().Returns(ports);
            var query = new GetAllPortQuery();
            GeneratePortsDto(ports);

            // Act
            var handler = new GetAllPortQueryHandler(repository, mapper);
            var result = handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            Assert.Equal(2, result.Result.Count);
            Assert.Equal(ports[0].Id, result.Result[0].Id);
        }

        private void GeneratePortsDto(List<Port> ports)
        {
            mapper.Map<List<PortDTO>>(ports).Returns(new List<PortDTO>
            {
                new PortDTO
                {
                    Id = ports[0].Id,
                    Name = ports[0].Name,
                    Country = ports[0].Country
                },
                new PortDTO
                {
                    Id = ports[1].Id,
                    Name = ports[1].Name,
                    Country = ports[1].Country
                }
            });
        }

        private List<Port> GeneratePorts()
        {
            return new List<Port>
            {
                new Port
                {
                    Id = Guid.NewGuid(),
                    Name = "Port of Los Angeles",
                    Country = "Los Angeles, USA"
                },
                new Port
                {
                    Id = Guid.NewGuid(),
                    Name = "Port of Shanghai",
                    Country = "Shanghai, China"
                }
            };
        }
    }
}
