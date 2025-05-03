using Application.DTOs;
using Application.Features.Port.Commands.CreatePort;
using Application.Features.Port.Commands.UpdatePort;
using Application.Features.Ship.Commands.CreateShip;
using Application.Features.Ship.Commands.UpdateShip;
using Application.Features.Voyage.Commands.CreateVoyage;
using AutoMapper;
using Domain.Entities;

namespace Application.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ship,ShipDTO>().ReverseMap();
            CreateMap<CreateShipCommand,Ship>().ReverseMap();
            CreateMap<UpdateShipCommand,Ship>().ReverseMap();

            CreateMap<Port,PortDTO>().ReverseMap();
            CreateMap<CreatePortCommand, Port>().ReverseMap();
            CreateMap<UpdatePortCommand, Port>().ReverseMap();

            CreateMap<Voyage, VoyageDTO>().ReverseMap();
            CreateMap<CreateVoyageCommand, Voyage>().ReverseMap();
        }
    }
}
