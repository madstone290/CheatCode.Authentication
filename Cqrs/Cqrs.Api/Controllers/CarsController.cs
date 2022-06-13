using Cqrs.Application.Cars.Commands;
using Cqrs.Application.Cars.Queries;
using Cqrs.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cqrs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CarsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Car>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCars()
        {
            var result = await mediator.Send(new GetAllCarsQuery());

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCar()
        {
            var result = await mediator.Send(new CreateCarCommand());

            return Ok(result);
        }
    }
}
