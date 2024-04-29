using Microsoft.AspNetCore.Mvc;
using Challenge.Atm.Application.Requests;
using MediatR;
using Challenge.Atm.Application.Handlers;
using Challenge.Atm.Application.Request;
using Microsoft.AspNetCore.Authorization;

namespace Challenge.Atm.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CardController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
             var result = await _mediator.Send(new LoginCommand(loginRequest));

             return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] CardRequest request)
        {
            var result = await _mediator.Send(new CreateCardCommand(request));

            return Ok(result);
        }


        [HttpGet("balance")]
        [Authorize]
        public async Task<IActionResult> GetBalance()
        {
            var result = await _mediator.Send(new GetBalanceQuery());

            return Ok(result);
        }

        [HttpPost("movement")]
        [Authorize]
        public async Task<IActionResult> Extract([FromBody] CreateMovementRequest request)
        {
            var result = await _mediator.Send(new CreateMovementCommand(request));

            return Ok(result);
        }

        [HttpGet("history")]
        [Authorize]
        public async Task<IActionResult> GetHistory([FromQuery] GetHistoryParameters filters)
        {
            var result = await _mediator.Send(new GetHistoryCardQuery(filters.PageNumber, filters.PagedSize));

            return Ok(result);
        }


        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery]GetAllCardsParameters filters)
        {
            var result = await _mediator.Send(new GetAllCardsQuery(filters.PageNumber, filters.PagedSize, filters.Name));

            return Ok(result);
        }
    }
}
