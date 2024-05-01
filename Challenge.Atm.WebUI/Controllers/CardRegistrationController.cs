using Microsoft.AspNetCore.Mvc;
using Challenge.Atm.Application.Requests;
using MediatR;
using Challenge.Atm.Application.Request;
using Microsoft.AspNetCore.Authorization;
using Challenge.Atm.Application.Handlers.Commnads;
using Challenge.Atm.Application.Handlers.Queries;

namespace Challenge.Atm.WebUI.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardRegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CardRegistrationController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost("new")]
        public async Task<IActionResult> Add([FromBody] CardRequest request)
        {
            var result = await _mediator.Send(new CreateCardCommand(request));

            return Ok(result);
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery]GetAllCardsParameters filters)
        {
            var result = await _mediator.Send(new GetAllCardsQuery(filters.PageNumber, filters.PageSize, filters.Name));

            return Ok(result);
        }
    }
}
