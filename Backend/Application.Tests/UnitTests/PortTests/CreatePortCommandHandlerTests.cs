using Application.Features.Port.Commands.CreatePort;
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

namespace Application.Tests.UnitTests.PortTests
{
    public class CreatePortCommandHandlerTests
    {
        private readonly IPortRepository repository;
        private readonly CreatePortCommandHandler handler;
        private readonly IMapper mapper;

        public CreatePortCommandHandlerTests()
        {
            repository = Substitute.For<IPortRepository>();
            mapper = Substitute.For<IMapper>();
            handler = new CreatePortCommandHandler(repository, mapper);
        }

        [Fact]
        public async Task Given_ValidCreatePortCommand_When_HandleIsCalled_Then_PortShouldBeCreated()
        {
            // Arrange
            var command = new CreatePortCommand
            {
                Name = "Port of Austria",
                Country = "Austria"
            };

            var portEntity = new Domain.Entities.Port
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Country = command.Country
            };

            mapper.Map<Domain.Entities.Port>(command).Returns(portEntity);
            repository.AddAsync(portEntity).Returns(Result<Guid>.Success(portEntity.Id));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(portEntity);
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBe(Guid.Empty);
        }


        [Fact]
        public async Task Given_InvalidCreatePortCommand_When_HandleIsCalled_Then_PortShouldNotBeCreated()
        {
            // Arrange
            var command = new CreatePortCommand
            {
                Name = "",
                Country = "Austria"
            };

            var portEntity = new Domain.Entities.Port
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Country = command.Country
            };

            mapper.Map<Domain.Entities.Port>(command).Returns(portEntity);
            repository.AddAsync(portEntity).Returns(Result<Guid>.Failure("Invalid port data"));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(portEntity);
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Invalid port data");
        }

    }
}
