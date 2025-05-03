using Application.DTOs;
using Application.Features.Ship.Commands.CreateShip;
using Application.Features.Ship.Commands.DeleteShip;
using Application.Features.Ship.Commands.UpdateShip;
using Application.Features.Ship.Queries;
using Application.Features.Ship.Queries.GetAllShip;
using Application.Features.Ship.Queries.GetShipById;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShipManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShipsController : ControllerBase
    {

        private readonly IMediator mediator;

        public ShipsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Result<Guid>>> CreateShip(CreateShipCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetShipById), new { id = result.Data }, result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ShipDTO>>> GetAllShips()
        {
            var result = await mediator.Send(new GetAllShipQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ShipDTO>> GetShipById(Guid id)
        {
            return await mediator.Send(new GetShipByIdQuery { Id = id });
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateShip(Guid id, UpdateShipCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("This id should match with command id");
            }

            await mediator.Send(command);
            return StatusCode(StatusCodes.Status204NoContent);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteShip(Guid id)
        {
            await mediator.Send(new DeleteShipCommand { Id = id });
            return NoContent();
        }

    }
}
