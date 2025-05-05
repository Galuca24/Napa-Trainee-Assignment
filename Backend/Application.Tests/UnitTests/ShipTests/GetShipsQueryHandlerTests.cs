using Application.DTOs;
using Application.Features.Ship.Queries.GetAllShip;
using AutoMapper;
using Domain.Entities;
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
    public class GetShipsQueryHandlerTests
    {
        private readonly IShipRepository repository;
        private readonly IMapper mapper;

        public GetShipsQueryHandlerTests()
        {
            repository = Substitute.For<IShipRepository>();
            mapper = Substitute.For<IMapper>();
        }


        [Fact]
      public void  Given_GetShipQueryHandler_When_HandleIsCalled_Then_AListOfShipsShouldBeReturned()
        {
            // Arrange
            List<Ship> ships = GenerateShips();
            repository.GetAllShipsAsync().Returns(ships);
            var query = new GetAllShipQuery();
            GenerateShipsDto(ships);

            //Act
            var handler = new GetAllShipQueryHandler(repository, mapper);
            var result = handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            Assert.Equal(2, result.Result.Count);
            Assert.Equal(ships[0].Id, result.Result[0].Id);

        }


        private void GenerateShipsDto(List<Ship> ships)
        {
            mapper.Map<List<ShipDTO>>(ships).Returns(new List<ShipDTO>
            {
                new ShipDTO
                {
                    Id = ships[0].Id,
                    Name = ships[0].Name,
                    MaxSpeed = ships[0].MaxSpeed
                },
                new ShipDTO
                {
                    Id = ships[1].Id,
                    Name = ships[1].Name,
                    MaxSpeed = ships[1].MaxSpeed
                }
            });
            
        }


        private List<Ship> GenerateShips()
        {
            return new List<Ship>
            {
                new Ship
                {
                    Id = Guid.NewGuid(),
                    Name = "Titanic",
                    MaxSpeed = 30
                },
                new Ship
                {
                    Id = Guid.NewGuid(),
                    Name = "Queen Mary",
                    MaxSpeed = 25
                }
            };
        }
    }
}
