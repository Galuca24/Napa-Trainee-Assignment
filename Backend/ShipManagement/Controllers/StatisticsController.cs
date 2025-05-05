using Application.DTOs;
using Application.Features.Port.Queries.GetMostArrivals;
using Application.Features.Ship.Queries.GetMostUsedShips;
using Application.Features.Voyage.Queries.GetAverageDuration;
using Application.Features.Voyage.Queries.GetAverageDurationPerMonth;
using Application.Features.Voyage.Queries.GetVisitedCountriesInLastYear;
using Application.Features.Voyage.Queries.GetVoyageCountPerMonth;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShipManagementAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator mediator;

        public StatisticsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("visited-countries-detailed-last-year")]
        public async Task<ActionResult<List<VisitedCountryDTO>>> GetVisitedCountriesDetailed()
        {
            var result = await mediator.Send(new GetVisitedCountriesQuery());
            return Ok(result);
        }

        [HttpGet("ships/most-used")]
        public async Task<ActionResult<List<ShipUsageDTO>>> GetMostUsedShips()
        {
            var result = await mediator.Send(new GetMostUsedShipsQuery());
            return Ok(result);
        }

        [HttpGet("voyages/count-per-month")]
        public async Task<ActionResult<List<VoyageCountPerMonthDTO>>> GetVoyageCountPerMonth()
        {
            var result = await mediator.Send(new GetVoyageCountPerMonthQuery());
            return Ok(result);
        }


        [HttpGet("voyages/average-duration")]
        public async Task<ActionResult<AverageDurationDTO>> GetAverageDuration()
        {
            var result = await mediator.Send(new GetAverageDurationQuery());
            return Ok(result);
        }

        [HttpGet("voyages/average-duration-per-month")]
        public async Task<ActionResult<List<AverageDurationPerMonthDTO>>> GetAverageDurationPerMonth()
        {
            var result = await mediator.Send(new GetAverageDurationPerMonthQuery());
            return Ok(result);
        }

        [HttpGet("ports/most-arrivals")]
        public async Task<ActionResult<List<MostArrivalsDTO>>> GetMostArrivals()
        {
            var result = await mediator.Send(new GetMostArrivalsQuery());
            return Ok(result);
        }


    }
}
