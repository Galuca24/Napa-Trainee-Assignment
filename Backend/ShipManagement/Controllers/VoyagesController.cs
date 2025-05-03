using Application.DTOs;
using Application.Features.Voyage.Commands.CreateVoyage;
using Application.Features.Voyage.Queries.GetAllVoyage;
using Application.Features.Voyage.Queries.GetByIdVoyage;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShipManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VoyagesController : ControllerBase
    {
        private readonly IMediator mediator;
        public VoyagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Result<Guid>>> CreateVoyage(CreateVoyageCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetVoyageById), new { id = result.Data }, result);
        }

        [HttpGet]
        public IActionResult GetAllVoyages()
        {
            var result = mediator.Send(new GetAllVoyageQuery()).Result;
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<VoyageDTO>> GetVoyageById(Guid id)
        {
            var result = await mediator.Send(new GetByIdVoyageQuery { Id = id });
            return Ok(result);

        }
    }
}
