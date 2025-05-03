using Application.DTOs;
using Application.Features.Port.Commands.CreatePort;
using Application.Features.Port.Commands.DeletePort;
using Application.Features.Port.Commands.UpdatePort;
using Application.Features.Port.Queries.GetAllPort;
using Application.Features.Port.Queries.GetPortById;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShipManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PortsController : ControllerBase

    {
        private readonly IMediator mediator;

        public PortsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Result<Guid>>> CreatePort(CreatePortCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetPortById), new { id = result.Data }, result);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdatePort(Guid id, UpdatePortCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("This id should match with command id");
            }

            await mediator.Send(command);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeletePort(Guid id)
        {
            await mediator.Send(new DeletePortCommand { Id = id });
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpGet]
        public IActionResult GetAllPorts()
        {
            var result = mediator.Send(new GetAllPortQuery()).Result;
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PortDTO>> GetPortById(Guid id)
        {
            var result = await mediator.Send(new GetPortByIdQuery { Id = id });
            return Ok(result);
        }
    }
}
