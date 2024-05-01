using Challenge.Atm.Application.Handlers.Commnads;
using Challenge.Atm.Application.Handlers.Queries;
using Challenge.Atm.Application.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Challenge.Atm.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> Extract([FromBody] CreateTransactionRequest request)
        {
            var result = await _mediator.Send(new CreateTransactionCommand(request));

            return Created(HttpContext.Request.Path.Value,result);
        }

        [HttpGet("history")]
        [Authorize]
        public async Task<IActionResult> GetHistory([FromQuery] GetHistoryParameters filters)
        {
            var result = await _mediator.Send(new GetHistoryTransactionsQuery(filters.PageNumber, filters.PageSize));

            return Ok(result);
        }

    }
}
