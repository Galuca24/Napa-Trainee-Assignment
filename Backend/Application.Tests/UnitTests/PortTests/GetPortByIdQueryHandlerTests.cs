using Application.DTOs;
using Application.Features.Port.Queries.GetPortById;
using AutoMapper;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.UnitTests.PortTests
{
    public class GetPortByIdQueryHandlerTests
    {
        private readonly IPortRepository repository;
        private readonly IMapper mapper;
        private readonly GetPortByIdQueryHandler handler;

        public GetPortByIdQueryHandlerTests()
        {
            repository = Substitute.For<IPortRepository>();
            mapper = Substitute.For<IMapper>();
            handler = new GetPortByIdQueryHandler(repository, mapper);
        }

        [Fact]
        public async Task Given_ValidGetPortByIdQuery_When_HandleIsCalled_Then_PortShouldBeReturned()
        {
            // Arrange
            var portId = Guid.NewGuid();
            var query = new GetPortByIdQuery { Id = portId };
            var portEntity = new Domain.Entities.Port
            {
                Id = portId,
                Name = "Port of Austria",
                Country = "Austria"
            };

            repository.GetByIdAsync(portId).Returns(portEntity);
            mapper.Map<PortDTO>(portEntity).Returns(new PortDTO
            {
                Id = portEntity.Id,
                Name = portEntity.Name,
                Country = portEntity.Country
            });

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(portId);
        }
    }
}
